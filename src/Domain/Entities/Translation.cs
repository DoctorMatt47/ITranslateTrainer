using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Translation : HasId<int>
{
    public required TranslationText OriginText { get; init; } = null!;
    public required TranslationText TranslationText { get; init; } = null!;

    public int OriginTextId { get; private init; } = 0;
    public int TranslationTextId { get; private init; } = 0;

    public int UserId { get; private init; } = 0;
    public User User { get; private init; } = null!;
}
