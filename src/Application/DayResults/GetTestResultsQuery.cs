using AutoMapper;
using AutoMapper.QueryableExtensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Tests;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.DayResults;

public record GetDayResultsQuery : IRequest<IEnumerable<GetDayResultResponse>>;

public class GetDayResultsQueryHandler : IRequestHandler<GetDayResultsQuery, IEnumerable<GetDayResultResponse>>
{
    private readonly ITranslateDbContext _context;
    private readonly IMapper _mapper;

    public GetDayResultsQueryHandler(ITranslateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetDayResultResponse>> Handle(
        GetDayResultsQuery request,
        CancellationToken cancellationToken) =>
        await _context.Set<DayResult>()
            .OrderBy(d => d.Day)
            .ProjectTo<GetDayResultResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}
