namespace ITranslateTrainer.Application.TranslationSheet;

public record ParseTranslationResponse(
    string FirstLanguage,
    string SecondLanguage,
    string FirstText,
    string SecondText);
