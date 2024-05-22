using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.Tests;

public record AnswerOnTestCommand(
    int Id,
    int OptionId)
    : IRequest<AnswerOnTestResponse>;

public class AnswerOnTestCommandHandler(IAppDbContext dbContext)
    : IRequestHandler<AnswerOnTestCommand, AnswerOnTestResponse>
{
    public async Task<AnswerOnTestResponse> Handle(AnswerOnTestCommand request, CancellationToken cancellationToken)
    {
        var test = await dbContext.Set<Test>().FindOrThrowAsync(request.Id, cancellationToken);

        var correctOptionId = test.Options.FirstOrDefault(o => o.IsCorrect)?.Id ?? -1;
        var response = new AnswerOnTestResponse(correctOptionId);

        if (test.IsAnswered)
        {
            return response;
        }

        test.SetAnswer(request.OptionId);
        await dbContext.SaveChangesAsync(cancellationToken);

        return response;
    }
}
