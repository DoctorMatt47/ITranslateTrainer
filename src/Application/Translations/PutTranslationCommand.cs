using ITranslateTrainer.Application.Common.Interfaces;
using MediatR;

namespace ITranslateTrainer.Application.Translations;

public record PutTranslationCommand(
        string FirstText,
        string FirstLanguage,
        string SecondText,
        string SecondLanguage)
    : IRequest<TranslationResponse>;

internal class PutTranslationCommandHandler : IRequestHandler<PutTranslationCommand, TranslationResponse>
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public PutTranslationCommandHandler(
        IMediator mediator,
        ITranslateDbContext context)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<TranslationResponse> Handle(PutTranslationCommand request, CancellationToken cancellationToken)
    {
        var (firstText, firstLanguage, secondText, secondLanguage) = request;

        var getOrCreate = new GetOrCreateTranslation(firstText, firstLanguage, secondText, secondLanguage);
        var translation = await _mediator.Send(getOrCreate, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return translation.ToResponse();
    }
}
