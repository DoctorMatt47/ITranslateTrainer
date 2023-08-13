namespace ITranslateTrainer.Domain.Abstractions;

public interface IHasId<out T> where T : IEquatable<T>
{
    T Id { get; }
}
