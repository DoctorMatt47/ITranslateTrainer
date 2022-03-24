using ITranslateTrainer.Application.Common.Behaviours;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Texts.Requests;
using ITranslateTrainer.Application.Translations.Requests;
using MediatR;

namespace ITranslateTrainer.Application.Translations.Commands;

public record CreateTranslationCommand(CreateText FirstText, CreateText SecondText) :
    IRequest<IntIdResponse>, ITransaction;

public record CreateTranslationCommandHandler(IMediator _mediator) :
    IRequestHandler<CreateTranslationCommand, IntIdResponse>
{
    private readonly IMediator _mediator = _mediator;

    public async Task<IntIdResponse> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
    {
        var (firstText, secondText) = request;
        var translation = await _mediator.Send(new CreateTranslation(firstText, secondText),
            cancellationToken);
        return new IntIdResponse(translation.Id);
    }
}
