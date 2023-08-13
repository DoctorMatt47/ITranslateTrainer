using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Option : IHasId<int>
{
    protected Option()
    {
    }

    public Option(TranslationText translationText, Test test, bool isCorrect)
    {
        TranslationText = translationText;
        Test = test;
        IsCorrect = isCorrect;
    }

    public int TextId { get; protected set; }
    public TranslationText TranslationText { get; protected set; } = null!;

    public int TestId { get; protected set; }
    public Test Test { get; protected set; } = null!;

    public bool IsCorrect { get; protected set; }

    public bool IsChosen { get; protected set; }

    public int Id { get; protected set; }

    public void Choose()
    {
        IsChosen = true;
    }
}
