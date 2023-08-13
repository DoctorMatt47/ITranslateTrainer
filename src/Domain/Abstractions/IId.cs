namespace ITranslateTrainer.Domain.Abstractions;

public interface IId<out T>
{
    T Value { get; }
}
