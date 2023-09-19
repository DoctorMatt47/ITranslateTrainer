using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record GetTestQuery(int Id) : IRequest<TestResponse>;

internal record GetTestQueryHandler : IRequestHandler<GetTestQuery, TestResponse>
{
    private readonly ITranslateDbContext _context;

    public GetTestQueryHandler(ITranslateDbContext context) => _context = context;

    public async Task<TestResponse> Handle(GetTestQuery request, CancellationToken cancellationToken)
    {
        var test = await _context.Set<Test>()
                .Include(t => t.Text)
                .Include(t => t.Options)
                .ThenInclude(t => t.Text)
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"There is no test with id: {request.Id}");

        if (!test.IsAnswered) throw new BadRequestException("You don't have permission to get not answered test");

        return test.ToResponse();
    }
}
