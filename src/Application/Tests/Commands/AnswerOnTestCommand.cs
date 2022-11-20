using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests.Commands;

public record AnswerOnTestCommand(int Id, int OptionId) : IRequest;

internal class AnswerOnTestCommandHandler : IRequestHandler<AnswerOnTestCommand>
{
    private readonly ITranslateDbContext _dbContext;

    public AnswerOnTestCommandHandler(ITranslateDbContext dbContext) => _dbContext = dbContext;

    public async Task<Unit> Handle(AnswerOnTestCommand request, CancellationToken cancellationToken)
    {
        var test = await _dbContext.Set<Test>()
            .Include(t => t.Options)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (test is null) throw new NotFoundException($"There is no test with id: {request.Id}");

        var option = test.Options.FirstOrDefault(o => o.Id == request.OptionId);
        if (option is null) throw new NotFoundException($"There is no option with id: {request.OptionId}");

        test.IsAnswered = true;
        option.IsChosen = true;

        await _dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}
