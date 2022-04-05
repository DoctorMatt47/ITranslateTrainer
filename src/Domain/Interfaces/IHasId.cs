namespace ITranslateTrainer.Domain.Interfaces;

public interface IHasId<out T> where T : IEquatable<T>
{
    public T Id { get; }
}