namespace ITranslateTrainer.Application.Common.Extensions;

public static class LinqExtensions
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
        var random = new Random();
        return enumerable.OrderBy(_ => random.Next()).ToList();
    }
}
