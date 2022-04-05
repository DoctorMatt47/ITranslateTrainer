using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.ValueObjects;

public abstract class UintIdBase : IHasId<uint>
{
    public uint Id { get; protected set; }
}