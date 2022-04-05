using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Languages.Handlers;
using ITranslateTrainer.Application.Texts.Extensions;
using ITranslateTrainer.Application.Translations.Handlers;
using ITranslateTrainer.Application.TranslationSheet.Requests;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.TranslationSheet.Commands;

public record ImportTranslationSheetCommand(Stream SheetStream) : ICommand<IEnumerable<object>>;

public record ImportTranslationSheetCommandHandler(IMediator _mediator) :
    IRequestHandler<ImportTranslationSheetCommand, IEnumerable<object>>
{
    private readonly IMediator _mediator = _mediator;

    public async Task<IEnumerable<object>> Handle(ImportTranslationSheetCommand request,
        CancellationToken cancellationToken)
    {
        var parsedTranslations =
            await _mediator.Send(new ParseTranslationSheet(request.SheetStream), cancellationToken);

        var response = new List<object>();
        foreach (var parsedTranslation in parsedTranslations)
        {
            var (firstLanguage, secondLanguage, firstText, secondText) = parsedTranslation;
            try
            {
                var firstTextFiltered = await _mediator.Send(new FilterText(firstText), cancellationToken);
                var secondTextFiltered = await _mediator.Send(new FilterText(secondText), cancellationToken);

                var firstLanguageFiltered =
                    await _mediator.Send(new ParseLanguage(firstLanguage), cancellationToken);
                var secondLanguageFiltered =
                    await _mediator.Send(new ParseLanguage(secondLanguage), cancellationToken);

                var translation = await _mediator.Send(new CreateTranslation(firstTextFiltered, firstLanguageFiltered,
                    secondTextFiltered, secondLanguageFiltered), cancellationToken);

                response.Add(translation);
            }
            catch (BadRequestException e)
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
