using ITranslateTrainer.Application.TranslationTexts;

namespace ITranslateTrainer.Application.Translations;

public record TranslationResponse
{
    public required int Id { get; init; }
    public required TextResponse OriginText { get; init; }
    public required TextResponse TranslationText { get; init; }
}
