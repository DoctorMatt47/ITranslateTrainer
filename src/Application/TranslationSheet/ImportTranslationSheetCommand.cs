using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using OneOf;

namespace ITranslateTrainer.Application.TranslationSheet;

public record ImportTranslationSheetCommand(Stream SheetStream)
    : IRequest<IEnumerable<OneOf<TranslationResponse, ErrorResponse>>>;

public class ImportTranslationSheetCommandHandler
    : IRequestHandler<ImportTranslationSheetCommand, IEnumerable<OneOf<TranslationResponse, ErrorResponse>>>
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;
    private readonly ITranslationSheetService _sheetService;

    public ImportTranslationSheetCommandHandler(
        IMediator mediator,
        ITranslationSheetService sheetService,
        ITranslateDbContext context)
    {
        _context = context;
        _mediator = mediator;
        _sheetService = sheetService;
    }

    public async Task<IEnumerable<OneOf<TranslationResponse, ErrorResponse>>> Handle(
        ImportTranslationSheetCommand request,
        CancellationToken cancellationToken)
    {
        var translations = await _sheetService.ParseTranslations(request.SheetStream);

        var tasks = translations
            .Select(async t => await TryGetOrCreateTranslation(t, cancellationToken))
            .ToList();

        foreach (var task in tasks)
        {
            await task;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return tasks
            .Select(task => task.Result)
            .Select(result => result.MapT0(translation => translation.ToResponse()));
    }

    private async Task<OneOf<Translation, ErrorResponse>> TryGetOrCreateTranslation(
        ParseTranslationResponse translationResponse,
        CancellationToken cancellationToken)
    {
        var (firstLanguage, secondLanguage, firstText, secondText) = translationResponse;

        try
        {
            var request = new GetOrCreateTranslation(firstText, firstLanguage, secondText, secondLanguage);
            return await _mediator.Send(request, cancellationToken);
        }
        catch (Exception e) when (e is BadRequestException or ArgumentException)
        {
            return new ErrorResponse(e.Message);
        }
    }
}
