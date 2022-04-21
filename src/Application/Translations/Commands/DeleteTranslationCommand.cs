using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Texts.Requests;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Commands;

public record DeleteTranslationCommand(uint Id) : IRequest, ITransactional;

public record DeleteTranslationCommandHandler(ITranslateDbContext _context, IMediator _mediator) :
    IRequestHandler<DeleteTranslationCommand>
{
    private readonly ITranslateDbContext _context = _context;
    private readonly IMediator _mediator = _mediator;

    public async Task<Unit> Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
    {
        var translationToDelete = await _context.Set<Translation>().Include(t => t.First).Include(t => t.Second)
            .FirstAsync(t => t.Id == request.Id, cancellationToken);

        _context.Set<Translation>().Remove(translationToDelete);

        var firstTextTranslationsRequest = new GetTranslationTextsByTextIdRequest(translationToDelete.FirstId);
        var firstTextTranslations = await _mediator.Send(firstTextTranslationsRequest, cancellationToken);

        if (firstTextTranslations.Count() <= 1) _context.Set<Text>().Remove(translationToDelete.First);

        var secondTextTranslationsRequest = new GetTranslationTextsByTextIdRequest(translationToDelete.SecondId);
        var secondTextTranslations = await _mediator.Send(secondTextTranslationsRequest, cancellationToken);

        if (secondTextTranslations.Count() <= 1) _context.Set<Text>().Remove(translationToDelete.Second);

        return Unit.Value;
    }
}

public record DeleteTranslationCommandValidateBehaviour(ITranslateDbContext _context) :
    IPipelineBehavior<DeleteTranslationCommand>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<Unit> Handle(DeleteTranslationCommand request, CancellationToken cancellationToken,
        RequestHandlerDelegate<Unit> next)
    {
        var isExist = await _context.Set<Translation>().AnyAsync(t => t.Id == request.Id, cancellationToken);
        if (!isExist) throw new BadRequestException($"There is no translation with id = {request.Id}");
        return await next.Invoke();
    }
}
