using ITranslateTrainer.Application.Common.Interfaces;
using MediatR;

namespace ITranslateTrainer.Application.Translations;

public record PutTranslationCommand(
        string FirstText,
        string FirstLanguage,
        string SecondText,
        string SecondLanguage)
    : IRequest<TranslationResponse>;

internal class PutTranslationCommandHandler(
        ISender mediator,
        ITranslateDbContext context)
    : IRequestHandler<PutTranslationCommand, TranslationResponse>
{
    public async Task<TranslationResponse> Handle(PutTranslationCommand request, CancellationToken cancellationToken)
    {
        var (firstText, firstLanguage, secondText, secondLanguage) = request;

        var getOrCreate = new GetOrCreateTranslation(firstText, firstLanguage, secondText, secondLanguage);
        var translation = await mediator.Send(getOrCreate, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return translation.ToResponse();
    }
}
