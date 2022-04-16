using AutoMapper;
using AutoMapper.QueryableExtensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Translations.Responses;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Queries;

public record GetTranslationsQuery : IRequest<IEnumerable<GetTranslationResponse>>;

public record GetTranslationsQueryHandler(ITranslateDbContext _context, IMapper _mapper) :
    IRequestHandler<GetTranslationsQuery, IEnumerable<GetTranslationResponse>>
{
    private readonly ITranslateDbContext _context = _context;
    private readonly IMapper _mapper = _mapper;

    public async Task<IEnumerable<GetTranslationResponse>> Handle(GetTranslationsQuery request,
        CancellationToken cancellationToken) =>
        await _context.Set<Translation>()
            .Include(t => t.First)
            .Include(t => t.Second)
            .ProjectTo<GetTranslationResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}