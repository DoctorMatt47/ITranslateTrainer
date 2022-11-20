using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests.Commands;

public record AnswerOnTestCommand(
        int Id,
        int OptionId)
    : IRequest;

internal class AnswerOnTestCommandHandler : IRequestHandler<AnswerOnTestCommand>
{
    private readonly ITranslateDbContext _dbContext;

    public AnswerOnTestCommandHandler(ITranslateDbContext dbContext) => _dbContext = dbContext;

    public async Task<Unit> Handle(AnswerOnTestCommand request, CancellationToken cancellationToken)
    {
        var test = await _dbContext.Set<Test>().FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        if (test is null) throw new NotFoundException($"There is no test with id: {request.Id}");

        var option = test.Options.FirstOrDefault(o => o.Id == request.OptionId);
        if (option is null) throw new NotFoundException($"There is no option with id: {request.OptionId}");

        var dateNow = DateOnly.FromDateTime(DateTime.UtcNow);

        var dayResult = await _dbContext.Set<DayResult>().FindAsync(dateNow);
        if (dayResult is null)
        {
            dayResult = new DayResult(dateNow);
            _dbContext.Set<DayResult>().Add(dayResult);
        }

        option.Chosen();
        test.GotAnswer();
        dayResult.GotAnswer(option.IsCorrect);

        await _dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}
