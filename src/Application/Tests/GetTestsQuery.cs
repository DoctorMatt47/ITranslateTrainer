using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record GetTestsQuery : IRequest<IEnumerable<TestResponse>>;

public class GetTestsQueryHandler(ITranslateDbContext context) 
    : IRequestHandler<GetTestsQuery, IEnumerable<TestResponse>>
{
    public async Task<IEnumerable<TestResponse>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
    {
        return await context.Set<Test>()
            .Where(Test.IsAnsweredExpression)
            .OrderByDescending(t => t.AnswerTime)
            .ProjectToResponse()
            .ToListAsync(cancellationToken);
    }
}
