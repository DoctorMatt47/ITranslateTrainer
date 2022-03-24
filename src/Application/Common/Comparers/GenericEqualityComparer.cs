namespace ITranslateTrainer.Application.Common.Comparers;

public class GenericEqualityComparer<T> : IEqualityComparer<T>
{
    private readonly Func<T, T, bool> _comparer;

    public GenericEqualityComparer(Func<T, T, bool> comparer) => _comparer = comparer;

    public bool Equals(T? x, T? y)
    {
        if (x is null && y is null) return true;
        if (x is null || y is null) return false;
        return _comparer(x, y);
    }

    public int GetHashCode(T obj)
    {
        if (obj is null) throw new ArgumentNullException(nameof(obj));
        return obj.GetHashCode();
    }
}