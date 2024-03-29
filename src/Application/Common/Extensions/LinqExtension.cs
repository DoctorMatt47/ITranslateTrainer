﻿using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Common.Extensions;

public static class LinqExtension
{
    public static IEnumerable<T> ExceptBy<T, TKey>(
        this IEnumerable<T> enumerable,
        IEnumerable<T> except,
        Func<T, TKey> field) => enumerable.ExceptBy(except.Select(field), field);

    public static IQueryable<T> Shuffle<T>(this IQueryable<T> collection)
    {
        return collection.OrderBy(_ => EF.Functions.Random());
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
        var random = new Random();
        return enumerable.OrderBy(_ => random.Next()).ToList();
    }
}
