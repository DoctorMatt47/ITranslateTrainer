namespace ITranslateTrainer.Domain.Abstractions;

public abstract record Id<T> : IId<T>
{
    public abstract T Value { get; init; }

    public static implicit operator T(Id<T> id) => id.Value;

    public static implicit operator Id<T>(T value)
    {
        dynamic id = Activator.CreateInstance<Id<T>>();
        id.Value = value!;
        return id;
    }
}
