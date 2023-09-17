using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Tests.Domain.Unit.Builders;

public abstract class HasIdBuilder<TValue, TId> : Builder<TValue> where TValue : HasId<TId>
{
    public HasIdBuilder<TValue, TId> WithId(TId id)
    {
        SetPublicInstanceProperty(nameof(HasId<TId>.Id), id!);
        return this;
    }
}
