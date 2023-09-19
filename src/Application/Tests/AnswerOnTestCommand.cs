using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record AnswerOnTestCommand(
        int Id,
        int OptionId)
    : IRequest<TestResponse>;

internal class AnswerOnTestCommandHandler : IRequestHandler<AnswerOnTestCommand, TestResponse>
{
    private readonly ITranslateDbContext _dbContext;

    public AnswerOnTestCommandHandler(ITranslateDbContext dbContext) => _dbContext = dbContext;

    public async Task<TestResponse> Handle(AnswerOnTestCommand request, CancellationToken cancellationToken)
    {
        var test = await _dbContext.Set<Test>().FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw NotFoundException.DoesNotExist(nameof(Test), request.Id);

        if (test.IsAnswered) return test.ToResponse();

        test.Answer(request.OptionId);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return test.ToResponse();
    }
}
