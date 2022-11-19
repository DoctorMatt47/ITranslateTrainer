using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.Texts.Commands;

public record PatchTextCommand(
        int Id,
        bool? CanBeOption,
        bool? CanBeTested)
    : IRequest;

internal class PatchTextCommandHandler : IRequestHandler<PatchTextCommand>
{
    private readonly ITranslateDbContext _context;

    public PatchTextCommandHandler(ITranslateDbContext context) => _context = context;

    public async Task<Unit> Handle(PatchTextCommand request, CancellationToken cancellationToken)
    {
        var (id, canBeOption, canBeTested) = request;

        var text = await _context.Set<Text>().FindAsync(id);

        if (canBeOption is not null) text!.CanBeOption = (bool) canBeOption;
        if (canBeTested is not null) text!.CanBeTested = (bool) canBeTested;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}

internal class PatchTextCommandValidateBehaviour : IPipelineBehavior<PatchTextCommand>
{
    private readonly ITranslateDbContext _context;

    public PatchTextCommandValidateBehaviour(ITranslateDbContext context) => _context = context;

    public async Task<Unit> Handle(
        PatchTextCommand request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<Unit> next)
    {
        var (id, canBeOption, canBeTested) = request;

        if (canBeOption is null && canBeTested is null) throw new BadRequestException("All values are null");

        var textExist = await _context.Set<Text>().FindAsync(id);
        if (textExist is null) throw new BadRequestException($"There is no text with id = {id}");

        return await next.Invoke();
    }
}
