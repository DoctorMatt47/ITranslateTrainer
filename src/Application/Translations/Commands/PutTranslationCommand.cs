using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations.Requests;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;
using MediatR;

namespace ITranslateTrainer.Application.Translations.Commands;

public record PutTranslationCommand(TextString FirstText, Language FirstLanguage, TextString SecondText,
    Language SecondLanguage) : IRequest<IntIdResponse>;

public class CreateTranslationCommandHandler : IRequestHandler<PutTranslationCommand, IntIdResponse>
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public CreateTranslationCommandHandler(IMediator mediator, ITranslateDbContext context)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<IntIdResponse> Handle(PutTranslationCommand request, CancellationToken cancellationToken)
    {
        var (firstText, firstLanguage, secondText, secondLanguage) = request;

        var getTranslation = new GetOrCreateTranslationRequest(firstText, firstLanguage, secondText, secondLanguage);
        var translation = await _mediator.Send(getTranslation, cancellationToken);

        await _context.SaveChangesAsync();

        return new IntIdResponse(translation.Id);
    }
}
