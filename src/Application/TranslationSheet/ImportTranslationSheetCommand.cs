using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Exceptions;
using MediatR;
using OneOf;

namespace ITranslateTrainer.Application.TranslationSheet;

public record ImportTranslationSheetCommand(Stream SheetStream)
    : IRequest<IEnumerable<OneOf<TranslationResponse, ErrorResponse>>>;

public class ImportTranslationSheetCommandHandler(
    ISender mediator,
    ITranslationSheetService sheetService,
    IAppDbContext context)
    : IRequestHandler<ImportTranslationSheetCommand, IEnumerable<OneOf<TranslationResponse, ErrorResponse>>>
{
    public async Task<IEnumerable<OneOf<TranslationResponse, ErrorResponse>>> Handle(
        ImportTranslationSheetCommand request,
        CancellationToken cancellationToken)
    {
        var translations = await sheetService.ParseTranslations(request.SheetStream);

        var tasks = translations
            .Select(async t => await TryGetOrCreateTranslation(t, cancellationToken))
            .ToList();

        foreach (var task in tasks) await task;

        await context.SaveChangesAsync(cancellationToken);

        return tasks
            .Select(task => task.Result)
            .Select(result => result.MapT0(translation => translation.ToResponse()));
    }

    private async Task<OneOf<Translation, ErrorResponse>> TryGetOrCreateTranslation(
        ParseTranslationResponse translation,
        CancellationToken cancellationToken)
    {
        try
        {
            return await mediator.Send(
                new GetOrCreateTranslation(
                    translation.OriginText.MapToRequest(),
                    translation.TranslationText.MapToRequest()),
                cancellationToken);
        }
        catch (Exception e) when (e is BadRequestException or ArgumentException)
        {
            return new ErrorResponse(e.Message);
        }
    }
}
