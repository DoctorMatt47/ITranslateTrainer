using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.Entities;

public class Option : IHasId<int>
{
    public Option(int textId, int testId)
    {
        TextId = textId;
        TestId = testId;
    }

    public int TextId { get; protected set; }
    public Text Text { get; protected set; } = null!;

    public int TestId { get; protected set; }
    public Test Test { get; protected set; } = null!;

    public bool IsChosen { get; set; }
    public bool IsCorrect { get; set; }

    public int Id { get; protected set; }
}
