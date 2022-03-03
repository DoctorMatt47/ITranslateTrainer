using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Languages.Queries;
using ITranslateTrainer.Application.Texts.Commands;
using ITranslateTrainer.Application.Texts.Queries;
using ITranslateTrainer.Application.Translations.Commands;
using ITranslateTrainer.Application.TranslationSheet.Queries;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;

namespace ITranslateTrainer.Application.TranslationSheet.Commands;

public record CreateTranslationSheetCommand(Stream SheetStream) : IRequest<IEnumerable<object>>;

public record CreateTranslationSheetCommandHandler(IMediator Mediator, ITranslateDbContext Context) :
    IRequestHandler<CreateTranslationSheetCommand, IEnumerable<object>>
{
    public async Task<IEnumerable<object>> Handle(CreateTranslationSheetCommand request,
        CancellationToken cancellationToken)
    {
        var parsedTranslations =
            await Mediator.Send(new ParseTranslationSheetQuery(request.SheetStream), cancellationToken);

        var response = new List<object>();
        foreach (var parsedTranslation in parsedTranslations)
        {
            var (firstLanguage, secondLanguage, firstText, secondText) = parsedTranslation;
            try
            {
                var firstTextFiltered = await Mediator.Send(new FilterTextQuery(firstText), cancellationToken);
                var secondTextFiltered = await Mediator.Send(new FilterTextQuery(secondText), cancellationToken);

                var firstLanguageFiltered =
                    await Mediator.Send(new ParseLanguageQuery(firstLanguage), cancellationToken);
                var secondLanguageFiltered =
                    await Mediator.Send(new ParseLanguageQuery(secondLanguage), cancellationToken);

                var translationId = await Mediator.Send(new PrepareCreationTranslationCommand(
                    new CreateTextCommand(firstTextFiltered, firstLanguageFiltered),
                    new CreateTextCommand(secondTextFiltered, secondLanguageFiltered)), cancellationToken);

                response.Add(translationId);
            }
            catch (BadRequestException e)
            {
                response.Add(new
                {
                    errorMessage = e.Message
                });
            }
        }

        await Context.SaveChangesAsync();
        return response;
    }
}