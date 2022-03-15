using AutoMapper;
using ITranslateTrainer.Application.Texts.Queries;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Queries;

public record GetTranslationsByTextIdQuery(int TextId) : IRequest<IEnumerable<GetTextResponse>>;

public record GetTranslationsByTextIdQueryHandler(ITranslateDbContext Context, IMapper Mapper) :
    IRequestHandler<GetTranslationsByTextIdQuery, IEnumerable<GetTextResponse>>
{
    public async Task<IEnumerable<GetTextResponse>> Handle(GetTranslationsByTextIdQuery request,
        CancellationToken cancellationToken)
    {
        var firstTexts = Context.Translations.Where(t => t.FirstId == request.TextId).Include(t => t.Second)
            .Select(t => t.Second);
        var secondTexts = Context.Translations.Where(t => t.SecondId == request.TextId).Include(t => t.Second)
            .Select(t => t.First);
        var texts = await firstTexts.Concat(secondTexts).ToListAsync(cancellationToken);

        return texts.Select(t => Mapper.Map<GetTextResponse>(t));
    }
}
