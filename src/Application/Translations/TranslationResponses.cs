using ITranslateTrainer.Application.TranslationTexts;

namespace ITranslateTrainer.Application.Translations;

public record TranslationResponse(
    int Id,
    TranslationTextResponse First,
    TranslationTextResponse Second);
