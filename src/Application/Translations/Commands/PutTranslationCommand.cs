using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations.Requests;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;
using MediatR;

namespace ITranslateTrainer.Application.Translations.Commands;

public record PutTranslationCommand(TextString FirstText, Language FirstLanguage, TextString SecondText,
    Language SecondLanguage) : IRequest<UintIdResponse>, ITransactional;

public record CreateTranslationCommandHandler(IMediator _mediator) :
    IRequestHandler<PutTranslationCommand, UintIdResponse>
{
    private readonly IMediator _mediator = _mediator;

    public async Task<UintIdResponse> Handle(PutTranslationCommand request, CancellationToken cancellationToken)
    {
        var (firstText, firstLanguage, secondText, secondLanguage) = request;

        var getOrCreateTranslationRequest =
            new GetOrCreateTranslationRequest(firstText, firstLanguage, secondText, secondLanguage);

        var translation = await _mediator.Send(getOrCreateTranslationRequest, cancellationToken);

        return new UintIdResponse(translation.Id);
    }
}
