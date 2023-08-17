namespace ITranslateTrainer.Domain.Abstractions;

public class HasId<T> : IHasId<T>
{
    public T Id { get; protected init; } = default!;
}
