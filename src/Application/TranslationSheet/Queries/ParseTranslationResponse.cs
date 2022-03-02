namespace ITranslateTrainer.Application.TranslationSheet.Queries;

public record ParseTranslationResponse(string? FirstLanguage, string? SecondLanguage, string? FirstText,
    string? SecondText);
