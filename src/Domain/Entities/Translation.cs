// ReSharper disable RedundantDefaultMemberInitializer

using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Translation : HasId<int>
{
    public required Text OriginText { get; init; } = null!;
    public required Text TranslationText { get; init; } = null!;

    public bool CanBeOption { get; set; } = true;

    public int OriginTextId { get; private init; } = 0;
    public int TranslationTextId { get; private init; } = 0;
    public int UserId { get; private init; } = 0;
    public User User { get; private init; } = null!;
}
