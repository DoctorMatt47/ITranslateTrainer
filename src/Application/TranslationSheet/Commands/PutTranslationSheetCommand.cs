using System.Collections;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations.Requests;
using ITranslateTrainer.Application.TranslationSheet.Responses;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;
using MediatR;

namespace ITranslateTrainer.Application.TranslationSheet.Commands;

public record PutTranslationSheetCommand(Stream SheetStream) : IRequest<IEnumerable>;

public record PutTranslationSheetCommandHandler(IMediator _mediator, ITranslationSheetService _sheetService,
    ITranslateDbContext _context) : IRequestHandler<PutTranslationSheetCommand, IEnumerable>
{
    private readonly ITranslateDbContext _context = _context;
    private readonly IMediator _mediator = _mediator;
    private readonly ITranslationSheetService _sheetService = _sheetService;

    public async Task<IEnumerable> Handle(PutTranslationSheetCommand request,
        CancellationToken cancellationToken)
    {
        var translations = await _sheetService.ParseTranslations(request.SheetStream);

        var response = new List<object>();
        foreach (var translation in translations)
            response.Add(await TryGetOrCreateTranslation(translation, cancellationToken));

        await _context.SaveChangesAsync();

        return response.Select(o => o is Translation t ? new UintIdResponse(t.Id) : o);
    }

    private async Task<object> TryGetOrCreateTranslation(ParseTranslationResponse translationResponse,
        CancellationToken cancellationToken)
    {
        var (firstLanguageString, secondLanguageString, firstTextString, secondTextString) = translationResponse;
        try
        {
            var firstLanguage = Enum.Parse<Language>(firstLanguageString);
            var secondLanguage = Enum.Parse<Language>(secondLanguageString);

            var firstText = TextString.From(firstTextString);
            var secondText = TextString.From(secondTextString);

            var getOrCreateTranslationRequest =
                new GetOrCreateTranslationRequest(firstText, firstLanguage, secondText, secondLanguage);

            var translation = await _mediator.Send(getOrCreateTranslationRequest, cancellationToken);

            return translation;
        }
        catch (Exception e) when (e is BadRequestException or ArgumentException)
        {
            return new {errorMessage = e.Message};
        }
    }
}
