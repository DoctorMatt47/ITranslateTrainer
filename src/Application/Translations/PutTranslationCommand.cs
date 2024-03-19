using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Texts;
using MediatR;

namespace ITranslateTrainer.Application.Translations;

public record PutTranslationCommand(
    TextRequest OriginText,
    TextRequest TranslationText)
    : IRequest<TranslationResponse>;

public class PutTranslationCommandHandler(
    ISender mediator,
    IAppDbContext context)
    : IRequestHandler<PutTranslationCommand, TranslationResponse>
{
    public async Task<TranslationResponse> Handle(PutTranslationCommand request, CancellationToken cancellationToken)
    {
        var translation = await mediator.Send(
            new GetOrCreateTranslation(request.OriginText, request.TranslationText),
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return translation.ToResponse();
    }
}
