using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations.Requests;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using MediatR;

namespace ITranslateTrainer.Application.TranslationSheet.Commands;

public record ImportTranslationSheetCommand(Stream SheetStream) : IRequest<IEnumerable<object>>, ITransactional;

public record ImportTranslationSheetCommandHandler(IMediator _mediator, ITranslationSheetService _sheetService) :
    IRequestHandler<ImportTranslationSheetCommand, IEnumerable<object>>
{
    private readonly IMediator _mediator = _mediator;
    private readonly ITranslationSheetService _sheetService = _sheetService;

    public async Task<IEnumerable<object>> Handle(ImportTranslationSheetCommand request,
        CancellationToken cancellationToken)
    {
        var parsedTranslations = await _sheetService.ParseTranslations(request.SheetStream);

        var response = new List<object>();
        foreach (var parsedTranslation in parsedTranslations)
        {
            var (firstLanguageString, secondLanguageString, firstText, secondText) = parsedTranslation;
            try
            {
                var firstLanguage = Enum.Parse<Language>(firstLanguageString);
                var secondLanguage = Enum.Parse<Language>(secondLanguageString);

                var translation = await _mediator.Send(
                    new CreateTranslationRequest(firstText, firstLanguage, secondText, secondLanguage),
                    cancellationToken);

                response.Add(translation);
            }
            catch (Exception e) when (e is BadRequestException or ArgumentException)
            {
                response.Add(new
                {
                    errorMessage = e.Message
                });
            }
        }

        return response.Select(o => o is Translation t ? new UintIdResponse(t.Id) : o);
    }
}