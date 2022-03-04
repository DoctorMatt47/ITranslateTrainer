using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts.Commands;

public record PatchTextCommand(int Id, bool? CanBeOption, bool? CanBeTested) : IRequest;

public record PatchTextCommandHandler(ITranslateDbContext Context) : IRequestHandler<PatchTextCommand>
{
    public async Task<Unit> Handle(PatchTextCommand request, CancellationToken cancellationToken)
    {
        var (id, canBeOption, canBeTested) = request;

        var text = await Context.Texts.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        if (text is null) throw new BadRequestException($"There is no text with id = {id}");

        if (canBeOption is not null) text.CanBeOption = (bool) canBeOption;
        if (canBeTested is not null) text.CanBeTested = (bool) canBeTested;

        await Context.SaveChangesAsync();
        return Unit.Value;
    }
}
