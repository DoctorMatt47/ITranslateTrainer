using System.Collections;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.TranslationSheet;

public record PutTranslationSheetCommand(Stream SheetStream) : IRequest<IEnumerable>;

public class PutTranslationSheetCommandHandler : IRequestHandler<PutTranslationSheetCommand, IEnumerable>
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;
    private readonly ITranslationSheetService _sheetService;

    public PutTranslationSheetCommandHandler(
        IMediator mediator,
        ITranslationSheetService sheetService,
        ITranslateDbContext context)
    {
        _context = context;
        _mediator = mediator;
        _sheetService = sheetService;
    }

    public async Task<IEnumerable> Handle(
        PutTranslationSheetCommand request,
        CancellationToken cancellationToken)
    {
        var translations = (await _sheetService.ParseTranslations(request.SheetStream)).ToList();

        var response = await translations.SelectAsync(t => TryGetOrCreateTranslation(t, cancellationToken));

        await _context.SaveChangesAsync();

        return response.Select(o => o is Translation t ? new IntIdResponse(t.Id) : o);
    }

    private async Task<object> TryGetOrCreateTranslation(
        ParseTranslationResponse translationResponse,
        CancellationToken cancellationToken)
    {
        var (firstLanguage, secondLanguage, firstText, secondText) = translationResponse;
        try
        {
            var request = new GetOrCreateTranslation(firstText, firstLanguage, secondText, secondLanguage);
            var translation = await _mediator.Send(request, cancellationToken);

            return translation;
        }
        catch (Exception e) when (e is BadRequestException or ArgumentException)
        {
            return new {errorMessage = e.Message};
        }
    }
}
