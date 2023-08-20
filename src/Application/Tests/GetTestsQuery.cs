using AutoMapper;
using AutoMapper.QueryableExtensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record GetTestsQuery : IRequest<IEnumerable<TestResponse>>;

internal class GetTestsQueryHandler : IRequestHandler<GetTestsQuery, IEnumerable<TestResponse>>
{
    private readonly ITranslateDbContext _context;
    private readonly IMapper _mapper;

    public GetTestsQueryHandler(ITranslateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TestResponse>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Set<Test>()
            .Where(Test.IsAnsweredExpression)
            .OrderByDescending(t => t.AnswerTime)
            .ProjectTo<TestResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
