namespace ITranslateTrainer.Domain.Abstractions;

public class HasId<T> : IHasId<T> where T : IEquatable<T>
{
    public T Id { get; protected init; } = default!;
}
