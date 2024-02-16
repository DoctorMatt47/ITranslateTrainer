using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.Tests;

public record AnswerOnTestCommand(int Id, int OptionId) : IRequest<TestResponse>;

public class AnswerOnTestCommandHandler(IAppDbContext dbContext)
    : IRequestHandler<AnswerOnTestCommand, TestResponse>
{
    public async Task<TestResponse> Handle(AnswerOnTestCommand request, CancellationToken cancellationToken)
    {
        var test = await dbContext.Set<Test>().FindOrThrowAsync(request.Id, cancellationToken);

        if (test.IsAnswered) return test.ToResponse();

        test.Answer(request.OptionId);
        await dbContext.SaveChangesAsync(cancellationToken);

        return test.ToResponse();
    }
}
