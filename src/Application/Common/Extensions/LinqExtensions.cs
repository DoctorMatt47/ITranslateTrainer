using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Common.Extensions;

public static class LinqExtensions
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
        var random = new Random();
        return enumerable.OrderBy(_ => random.Next()).ToList();
    }

    public static IQueryable<T> Shuffle<T>(this IQueryable<T> queryable)
    {
        return queryable.OrderBy(_ => EF.Functions.Random());
    }
}
