using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.Entities;

public class Translation : IHasId<int>
{
    public Translation(Text first, Text second)
    {
        First = first;
        Second = second;
    }

    public Translation(int firstId, int secondId)
    {
        FirstId = firstId;
        SecondId = secondId;
    }

    public int FirstId { get; protected set; }
    public int SecondId { get; protected set; }

    public Text First { get; protected set; } = null!;
    public Text Second { get; protected set; } = null!;

    public int Id { get; protected set; }
}
