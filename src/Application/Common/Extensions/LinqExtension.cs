using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Common.Extensions;

public static class LinqExtension
{
    public static bool Empty<T>(this IQueryable<T> queryable) => !queryable.Any();

    public static bool Empty<T>(this IEnumerable<T> enumerable) => !enumerable.Any();

    public static IQueryable<T> Shuffle<T>(this IQueryable<T> collection) =>
        collection.OrderBy(_ => EF.Functions.Random());

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
        var random = new Random();
        var array = enumerable.ToArray();
        var n = array.Length;
        while (n > 1)
        {
            var k = random.Next(n--);
            (array[n], array[k]) = (array[k], array[n]);
        }

        return array;
    }

    public static async Task<IEnumerable<TResponse>> WhenAllAsync<TResponse>(this IEnumerable<Task<TResponse>> tasks)
    {
        var responses = new List<TResponse>();
        foreach (var task in tasks)
        {
            var response = await task;
            responses.Add(response);
        }

        return responses;
    }

    public static async Task<IEnumerable<TResponse>> SelectAsync<TResponse, TRequest, TTaskResult>(
        this IEnumerable<TRequest> enumerable, Func<TRequest, TTaskResult, TResponse> map,
        Func<TRequest, Task<TTaskResult>> task)
    {
        var responses = new List<TResponse>();
        foreach (var element in enumerable)
        {
            var taskResult = await task(element);
            responses.Add(map(element, taskResult));
        }

        return responses;
    }
}
