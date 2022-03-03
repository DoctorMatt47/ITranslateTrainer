namespace ITranslateTrainer.Application.Common.Extensions;

public static class LinqExtension
{
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

    public static IQueryable<T> Shuffle<T>(this IQueryable<T> collection) => collection.OrderBy(x => Guid.NewGuid());
}
