using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.ValueObjects;

public abstract class IntIdBase : IHasId<int>
{
    public int Id { get; protected set; }
}
