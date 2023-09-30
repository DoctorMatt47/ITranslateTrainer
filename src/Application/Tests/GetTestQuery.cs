using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record GetTestQuery(int Id) : IRequest<TestResponse>;

public class GetTestQueryHandler(ITranslateDbContext context) : IRequestHandler<GetTestQuery, TestResponse>
{
    public async Task<TestResponse> Handle(GetTestQuery request, CancellationToken cancellationToken)
    {
        var test = await context.Set<Test>()
            .Include(t => t.Text)
            .Include(t => t.Options)
            .ThenInclude(t => t.Text)
            .FirstByIdOrThrowAsync(request.Id, cancellationToken);

        if (!test.IsAnswered) throw new BadRequestException("You don't have permission to get not answered test");

        return test.ToResponse();
    }
}
