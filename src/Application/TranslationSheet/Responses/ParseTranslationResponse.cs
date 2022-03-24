namespace ITranslateTrainer.Application.TranslationSheet.Responses;

public record ParseTranslationResponse(string? FirstLanguage, string? SecondLanguage, string? FirstText,
    string? SecondText);
