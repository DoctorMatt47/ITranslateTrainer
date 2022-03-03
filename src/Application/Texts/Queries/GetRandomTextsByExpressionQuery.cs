using System.Linq.Expressions;
using AutoMapper;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts.Queries;

public record GetRandomTextsByConditionQuery(Expression<Func<Text, bool>> Condition, int Count) :
    IRequest<IEnumerable<GetTextResponse>>;

public record GetRandomTextsByConditionQueryHandler(ITranslateDbContext Context, IMapper Mapper) :
    IRequestHandler<GetRandomTextsByConditionQuery, IEnumerable<GetTextResponse>>
{
    public async Task<IEnumerable<GetTextResponse>> Handle(GetRandomTextsByConditionQuery request,
        CancellationToken cancellationToken)
    {
        var (condition, count) = request;
        var randomTexts = await Context.Texts
            .Where(condition).Shuffle()
            .Take(count).ToListAsync(cancellationToken);

        return randomTexts.Select(t => Mapper.Map<GetTextResponse>(t));
    }
}
