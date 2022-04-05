using ITranslateTrainer.Domain.ValueObjects;

namespace ITranslateTrainer.Domain.Entities;

public class Translation : UintIdBase
{
    public Translation(Text first, Text second)
    {
        First = first;
        Second = second;
    }

    public Translation(uint firstId, uint secondId)
    {
        FirstId = firstId;
        SecondId = secondId;
    }

    public uint FirstId { get; protected set; }
    public uint SecondId { get; protected set; }

    public Text First { get; protected set; } = null!;
    public Text Second { get; protected set; } = null!;
}