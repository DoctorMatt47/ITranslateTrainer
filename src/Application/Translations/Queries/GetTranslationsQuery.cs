using AutoMapper;
using ITranslateTrainer.Application.Translations.Responses;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Interfaces;
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
        CancellationToken cancellationToken)
    {
        var translations = await _context.Set<Translation>().ToListAsync(cancellationToken);
        var textIds = translations.Select(t => new List<int> {t.FirstId, t.SecondId}).SelectMany(t => t).Distinct();
        await _context.Set<Text>().Where(t => textIds.Contains(t.Id)).ToListAsync(cancellationToken);
        return translations.Select(t => _mapper.Map<GetTranslationResponse>(t));
    }
}
