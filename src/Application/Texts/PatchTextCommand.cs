using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.Application.Texts;

public record PatchTextCommand : IRequest
{
    [FromQuery] public required int Id { get; init; }
    public required string Text { get; init; }
}

public class PatchTextCommandHandler(IAppDbContext context) : IRequestHandler<PatchTextCommand>
{
    public async Task Handle(PatchTextCommand request, CancellationToken cancellationToken)
    {
        var text = await context.Set<Text>().FindOrThrowAsync(request.Id, cancellationToken);
        text.Value = request.Text;
        await context.SaveChangesAsync(cancellationToken);
    }
}
