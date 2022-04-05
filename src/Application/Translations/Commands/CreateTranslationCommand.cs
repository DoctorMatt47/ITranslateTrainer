using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations.Handlers;
using ITranslateTrainer.Application.Translations.Requests;
using MediatR;

namespace ITranslateTrainer.Application.Translations.Commands;

public record CreateTranslationCommand(CreateTextRequest FirstText, CreateTextRequest SecondText) :
    ICommand<UintIdResponse>;

public record CreateTranslationCommandHandler(IMediator _mediator) :
    IRequestHandler<CreateTranslationCommand, UintIdResponse>
{
    private readonly IMediator _mediator = _mediator;

    public async Task<UintIdResponse> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
    {
        var ((firstText, firstLanguage), (secondText, secondLanguage)) = request;
        var translation = await _mediator.Send(new CreateTranslation(firstText, firstLanguage,
            secondText, secondLanguage), cancellationToken);
        return new UintIdResponse(translation.Id);
    }
}