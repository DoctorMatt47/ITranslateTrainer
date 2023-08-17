using AutoMapper;
using AutoMapper.QueryableExtensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations;

public record GetTranslationsQuery : IRequest<IEnumerable<TranslationResponse>>;

internal class GetTranslationsQueryHandler
    : IRequestHandler<GetTranslationsQuery,
        IEnumerable<TranslationResponse>>
{
    private readonly ITranslateDbContext _context;
    private readonly IMapper _mapper;

    public GetTranslationsQueryHandler(ITranslateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TranslationResponse>> Handle(
        GetTranslationsQuery query,
        CancellationToken cancellationToken)
    {
        return await _context.Set<Translation>()
            .Include(t => t.OriginText)
            .Include(t => t.TranslationText)
            .ProjectTo<TranslationResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
