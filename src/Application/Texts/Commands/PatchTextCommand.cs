using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts.Commands;

public record PatchTextCommand(int Id, bool? CanBeOption, bool? CanBeTested) : IRequest;

public record PatchTextCommandHandler(ITranslateDbContext _context) : IRequestHandler<PatchTextCommand>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<Unit> Handle(PatchTextCommand request, CancellationToken cancellationToken)
    {
        var (id, canBeOption, canBeTested) = request;

        var text = await _context.Set<Text>().FirstAsync(t => t.Id == id, cancellationToken);

        if (canBeOption is not null) text.CanBeOption = (bool) canBeOption;
        if (canBeTested is not null) text.CanBeTested = (bool) canBeTested;

        return Unit.Value;
    }
}

public record PatchTextCommandValidateBehaviour
    (ITranslateDbContext _context) : IPipelineBehavior<PatchTextCommand, Unit>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<Unit> Handle(PatchTextCommand request, CancellationToken cancellationToken,
        RequestHandlerDelegate<Unit> next)
    {
        var (id, canBeOption, canBeTested) = request;

        if (canBeOption is null && canBeTested is null) throw new BadRequestException("All values are null");

        var textExist = await _context.Set<Text>().AnyAsync(t => t.Id == id, cancellationToken);
        if (!textExist) throw new BadRequestException($"There is no text with id = {id}");

        return await next.Invoke();
    }
}