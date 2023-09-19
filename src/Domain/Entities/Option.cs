// ReSharper disable UnusedAutoPropertyAccessor.Local

using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Option : Entity<int>
{
    public required Text Text { get; init; }
    public required bool IsCorrect { get; init; }

    public bool IsChosen { get; internal set; }

    public int TextId { get; private set; }
    public int TestId { get; private set; }
    public Test Test { get; private set; } = null!;
}
