using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.Texts;

public record UpdateTextCommand(
    int Id,
    string Text)
    : IRequest;

public class UpdateTextCommandHandler(IAppDbContext context) : IRequestHandler<UpdateTextCommand>
{
    public async Task Handle(UpdateTextCommand request, CancellationToken cancellationToken)
    {
        var text = await context.Set<Text>().FindOrThrowAsync(request.Id, cancellationToken);
        text.Value = request.Text;
        await context.SaveChangesAsync(cancellationToken);
    }
}
