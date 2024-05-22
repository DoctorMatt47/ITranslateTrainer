// ReSharper disable UnusedAutoPropertyAccessor.Local

using System.ComponentModel.DataAnnotations.Schema;
using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Translation : EntityBase<int>
{
    [ForeignKey(nameof(OriginTextId))] public required Text OriginText { get; init; }
    [ForeignKey(nameof(TranslationTextId))] public required Text TranslationText { get; init; }

    public bool CanBeOption { get; set; } = true;

    public int OriginTextId { get; private init; }
    public int TranslationTextId { get; private init; }
}
