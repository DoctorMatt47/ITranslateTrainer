using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Texts.Commands;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;

namespace ITranslateTrainer.Application.Translations.Commands;

public record CreateTranslationCommand(CreateTextCommand FirstText, CreateTextCommand SecondText) :
    IRequest<IntIdResponse>;

public record CreateTranslationCommandHandler(IMediator Mediator, ITranslateDbContext Context) :
    IRequestHandler<CreateTranslationCommand, IntIdResponse>
{
    public async Task<IntIdResponse> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
    {
        var (firstText, secondText) = request;
        var translation = await Mediator.Send(new PrepareCreationTranslationCommand(firstText, secondText),
            cancellationToken);
        await Context.SaveChangesAsync();
        return new IntIdResponse(translation.Id);
    }
}
