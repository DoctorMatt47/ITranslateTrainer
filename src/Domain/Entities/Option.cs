using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.Entities;

public class Option : IHasId<int>
{
    public Option(int textId, int testId, bool isCorrect)
    {
        TextId = textId;
        TestId = testId;
        IsCorrect = isCorrect;
    }

    public int TextId { get; protected set; }
    public Text Text { get; protected set; } = null!;

    public int TestId { get; protected set; }
    public Test Test { get; protected set; } = null!;

    public bool IsCorrect { get; protected set; }

    public bool IsChosen { get; protected set; }

    public int Id { get; protected set; }

    public void Chosen()
    {
        IsChosen = true;
    }
}
