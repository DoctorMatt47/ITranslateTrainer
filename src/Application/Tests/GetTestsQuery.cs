using AutoMapper;
using AutoMapper.QueryableExtensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record GetTestsQuery : IRequest<IEnumerable<GetTestResponse>>;

internal class GetTestsQueryHandler : IRequestHandler<GetTestsQuery, IEnumerable<GetTestResponse>>
{
    private readonly ITranslateDbContext _context;
    private readonly IMapper _mapper;

    public GetTestsQueryHandler(ITranslateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetTestResponse>> Handle(GetTestsQuery request, CancellationToken cancellationToken) =>
        await _context.Set<Test>()
            .Where(Test.IsAnswered)
            .ProjectTo<GetTestResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}
