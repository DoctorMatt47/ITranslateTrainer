using ITranslateTrainer.Application.Texts;

namespace ITranslateTrainer.Application.Translations;

public record TranslationResponse(
    int Id,
    TextResponse OriginText,
    TextResponse TranslationText);
