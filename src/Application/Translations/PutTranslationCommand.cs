using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using MediatR;

namespace ITranslateTrainer.Application.Translations;

public record PutTranslationCommand(
        string FirstText,
        string FirstLanguage,
        string SecondText,
        string SecondLanguage)
    : IRequest<IntIdResponse>;

internal class PutTranslationCommandHandler : IRequestHandler<PutTranslationCommand, IntIdResponse>
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public PutTranslationCommandHandler(IMediator mediator, ITranslateDbContext context)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<IntIdResponse> Handle(PutTranslationCommand request, CancellationToken cancellationToken)
    {
        var (firstText, firstLanguage, secondText, secondLanguage) = request;

        var getOrCreate = new GetOrCreateTranslation(firstText, firstLanguage, secondText, secondLanguage);
        var translation = await _mediator.Send(getOrCreate, cancellationToken);

        await _context.SaveChangesAsync();

        return new IntIdResponse(translation.Id);
    }
}
