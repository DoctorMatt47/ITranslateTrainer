namespace ITranslateTrainer.Domain.Abstractions;

public interface IHasId<out T>
{
    T Id { get; }
}
