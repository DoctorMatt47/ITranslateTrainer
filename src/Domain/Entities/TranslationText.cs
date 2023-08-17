using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class TranslationText : HasId<int>
{
    public required string Text { get; init; } = null!;
    public required string Language { get; init; }

    public int TranslationId { get; private init; } = 0;
    public Translation Translation { get; private init; } = null!;
}
