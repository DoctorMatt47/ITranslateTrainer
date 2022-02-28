using AutoMapper;
using ITranslateTrainer.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Queries;

public record GetTranslationsQuery : IRequest<IEnumerable<GetTranslationResponse>>;

public record GetTranslationsQueryHandler(ITranslateDbContext Context, IMapper Mapper) :
    IRequestHandler<GetTranslationsQuery, IEnumerable<GetTranslationResponse>>
{
    public async Task<IEnumerable<GetTranslationResponse>> Handle(GetTranslationsQuery _,
        CancellationToken cancellationToken)
    {
        var translations = await Context.Translations.ToListAsync(cancellationToken);
        var textIds = translations.Select(t => new List<int> {t.FirstId, t.SecondId}).SelectMany(t => t).Distinct();
        await Context.Texts.Where(t => textIds.Contains(t.Id)).ToListAsync(cancellationToken);
        return translations.Select(t => Mapper.Map<GetTranslationResponse>(t));
    }
}
