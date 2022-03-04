using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Translations.Queries;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Commands;

public record DeleteTranslationCommand(int Id) : IRequest;

public record DeleteTranslationCommandHandler
    (ITranslateDbContext Context, IMediator Mediator) : IRequestHandler<DeleteTranslationCommand>
{
    public async Task<Unit> Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
    {
        var translationToDelete = await Context.Translations.Include(t => t.First).Include(t => t.Second)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (translationToDelete is null)
            throw new BadRequestException($"There is no translation with id = {request.Id}");

        Context.Translations.Remove(translationToDelete);

        var firstTextTranslations = await Mediator.Send(new GetTranslationsByTextIdQuery(translationToDelete.FirstId),
            cancellationToken);
        if (firstTextTranslations.Count() <= 1) Context.Texts.Remove(translationToDelete.First);

        var secondTextTranslations = await Mediator.Send(new GetTranslationsByTextIdQuery(translationToDelete.SecondId),
            cancellationToken);
        if (secondTextTranslations.Count() <= 1) Context.Texts.Remove(translationToDelete.Second);

        await Context.SaveChangesAsync();
        return Unit.Value;
    }
}
