using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Translation : HasId<int>
{
    public int FirstTextId { get; private init; } = 0;
    public int SecondTextId { get; private init; } = 0;
    public required TranslationText FirstText { get; init; } = null!;
    public required TranslationText SecondText { get; init; } = null!;
}
