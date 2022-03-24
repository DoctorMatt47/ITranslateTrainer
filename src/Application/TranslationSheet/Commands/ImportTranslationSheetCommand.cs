using ITranslateTrainer.Application.Common.Behaviours;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Languages.Requests;
using ITranslateTrainer.Application.Texts.Requests;
using ITranslateTrainer.Application.Translations.Requests;
using ITranslateTrainer.Application.TranslationSheet.Requests;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.TranslationSheet.Commands;

public record ImportTranslationSheetCommand(Stream SheetStream) : IRequest<IEnumerable<object>>, ITransaction;

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
                    await _mediator.Send(new ParseLanguageQuery(firstLanguage), cancellationToken);
                var secondLanguageFiltered =
                    await _mediator.Send(new ParseLanguageQuery(secondLanguage), cancellationToken);

                var translation = await _mediator.Send(new CreateTranslation(
                        new CreateText(firstTextFiltered, firstLanguageFiltered),
                        new CreateText(secondTextFiltered, secondLanguageFiltered)),
                    cancellationToken);

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

        return response.Select(o => o is Translation t ? new IntIdResponse(t.Id) : o);
    }
}
