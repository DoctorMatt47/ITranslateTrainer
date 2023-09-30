namespace ITranslateTrainer.Domain.Abstractions;

public abstract class EntityBase<T> : IHasId<T>
{
    public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;
    public T Id { get; protected init; } = default!;
}
