using AutoMapper;
using AutoMapper.QueryableExtensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts;

public record GetTextsQuery : IRequest<IEnumerable<GetTextResponse>>;

internal class GetTextsQueryHandler : IRequestHandler<GetTextsQuery, IEnumerable<GetTextResponse>>
{
    private readonly ITranslateDbContext _context;
    private readonly IMapper _mapper;

    public GetTextsQueryHandler(ITranslateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetTextResponse>> Handle(GetTextsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Set<Text>()
            .Where(t => t.CorrectCount + t.IncorrectCount != 0)
            .OrderByDescending(t => t.CorrectCount + t.IncorrectCount)
            .ProjectTo<GetTextResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
