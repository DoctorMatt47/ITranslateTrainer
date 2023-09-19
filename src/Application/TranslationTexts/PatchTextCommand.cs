using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Exceptions;
using MediatR;

namespace ITranslateTrainer.Application.TranslationTexts;

public record PatchTextCommand(
        int Id,
        string Text)
    : IRequest;

internal class PatchTextCommandHandler(ITranslateDbContext context) : IRequestHandler<PatchTextCommand>
{
    public async Task Handle(PatchTextCommand request, CancellationToken cancellationToken)
    {
        var text = await context.Set<Text>().FindAsync(new object?[] {request.Id}, cancellationToken)
            ?? throw new BadRequestException($"There is no text with id = {request.Id}");

        text.Value = request.Text;
        await context.SaveChangesAsync(cancellationToken);
    }
}
