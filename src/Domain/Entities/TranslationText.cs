namespace ITranslateTrainer.Domain.Entities;

public class TranslationText
{
    public required TranslationTextId Id { get; init; } = new TranslationTextId(1);
    public required string Text { get; init; } = null!;
    public required string Language { get; init; }
}
