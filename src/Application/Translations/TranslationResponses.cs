using ITranslateTrainer.Application.TranslationTexts;

namespace ITranslateTrainer.Application.Translations;

public record TranslationResponse(
    int Id,
    TextResponse First,
    TextResponse Second);
