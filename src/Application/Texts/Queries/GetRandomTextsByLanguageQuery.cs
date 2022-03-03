using AutoMapper;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts.Queries;

public record GetRandomTextsByLanguageQuery(Language Language, int Count) : IRequest<IEnumerable<GetTextResponse>>;

public record GetRandomTextsByLanguageQueryHandler(ITranslateDbContext Context, IMapper Mapper) :
    IRequestHandler<GetRandomTextsByLanguageQuery, IEnumerable<GetTextResponse>>
{
    public async Task<IEnumerable<GetTextResponse>> Handle(GetRandomTextsByLanguageQuery request,
        CancellationToken cancellationToken)
    {
        var (language, count) = request;
        var randomTexts = await Context.Texts
            .Where(t => t.Language == language && t.CanBeTested)
            .Shuffle().Take(count).ToListAsync(cancellationToken);

        return randomTexts.Select(t => Mapper.Map<GetTextResponse>(t));
    }
}
