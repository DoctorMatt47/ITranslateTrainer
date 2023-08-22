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

    public async Task<Unit> Handle(PatchTextCommand request, CancellationToken cancellationToken)
    {
        var text = await _context.Set<TranslationText>().FindAsync(request.Id)
            ?? throw new BadRequestException($"There is no text with id = {request.Id}");

        text.Text = request.Text;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
