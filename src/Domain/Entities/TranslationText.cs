// ReSharper disable RedundantDefaultMemberInitializer

using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class TranslationText : HasId<int>
{
    public required string Text { get; set; }
    public required string Language { get; init; }

    public int TranslationId { get; private init; } = 0;
    public Translation Translation { get; private init; } = null!;
}
