using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.Texts;

public record PatchTextCommand(
        int Id,
        string Text)
    : IRequest;

public class PatchTextCommandHandler(ITranslateDbContext context) : IRequestHandler<PatchTextCommand>
{
    public async Task Handle(PatchTextCommand request, CancellationToken cancellationToken)
    {
        var text = await context.Set<Text>().FindOrThrowAsync(request.Id, cancellationToken);
        text.Value = request.Text;
        await context.SaveChangesAsync(cancellationToken);
    }
}
