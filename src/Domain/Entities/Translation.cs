// ReSharper disable UnusedAutoPropertyAccessor.Local

using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Translation : Entity<int>
{
    public required Text OriginText { get; init; }
    public required Text TranslationText { get; init; }

    public bool CanBeOption { get; set; } = true;
    public int OriginTextId { get; private init; }
    public int TranslationTextId { get; private init; }
}
