// ReSharper disable RedundantDefaultMemberInitializer

using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Option : HasId<int>
{
    public required Text Text { get; init; }
    public required bool IsCorrect { get; init; }

    public bool IsChosen { get; internal set; }

    public int TextId { get; private set; } = 0;
    public int TestId { get; private set; } = 0;
    public Test Test { get; private set; } = null!;
}
