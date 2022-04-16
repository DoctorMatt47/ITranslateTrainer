namespace ITranslateTrainer.Domain.ValueObjects;

public abstract record WrapperBase<TWrapped>(TWrapped Value)
{
    public readonly TWrapped Value = Value;

    public static implicit operator TWrapped(WrapperBase<TWrapped> wrapper) => wrapper.Value;
}