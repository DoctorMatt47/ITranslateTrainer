using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.TranslationTexts;

public record PatchTextCommand(
        int Id,
        string Text)
    : IRequest;

internal class PatchTextCommandHandler : IRequestHandler<PatchTextCommand>
{
    private readonly ITranslateDbContext _context;

    public PatchTextCommandHandler(ITranslateDbContext context) => _context = context;

    public async Task Handle(PatchTextCommand request, CancellationToken cancellationToken)
    {
        var text = await _context.Set<Text>().FindAsync(new object?[] {request.Id}, cancellationToken)
            ?? throw new BadRequestException($"There is no text with id = {request.Id}");

        text.Value = request.Text;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
