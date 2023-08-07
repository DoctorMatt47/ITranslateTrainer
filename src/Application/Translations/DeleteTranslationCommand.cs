using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Texts;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations;

public record DeleteTranslationCommand(int Id) : IRequest;

internal class DeleteTranslationCommandHandler : IRequestHandler<DeleteTranslationCommand>
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public DeleteTranslationCommandHandler(ITranslateDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
    {
        var translationToDelete = await _context.Set<Translation>()
            .Include(t => t.First).Include(t => t.Second)
            .FirstAsync(t => t.Id == request.Id, cancellationToken);

        _context.Set<Translation>().Remove(translationToDelete);

        var firstRequest = new GetTranslationTextsByTextId(translationToDelete.FirstId);
        var firstTextTranslations = await _mediator.Send(firstRequest, cancellationToken);
        if (firstTextTranslations.Count() <= 1) _context.Set<TranslationText>().Remove(translationToDelete.First);

        var secondRequest = new GetTranslationTextsByTextId(translationToDelete.SecondId);
        var secondTextTranslations = await _mediator.Send(secondRequest, cancellationToken);
        if (secondTextTranslations.Count() <= 1) _context.Set<TranslationText>().Remove(translationToDelete.Second);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}

public class DeleteTranslationCommandValidateBehaviour : IPipelineBehavior<DeleteTranslationCommand>
{
    private readonly ITranslateDbContext _context;

    public DeleteTranslationCommandValidateBehaviour(ITranslateDbContext context) => _context = context;

    public async Task<Unit> Handle(
        DeleteTranslationCommand request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<Unit> next)
    {
        var isExist = await _context.Set<Translation>().AnyAsync(t => t.Id == request.Id, cancellationToken);
        if (!isExist) throw new BadRequestException($"There is no translation with id = {request.Id}");

        return await next.Invoke();
    }
}
