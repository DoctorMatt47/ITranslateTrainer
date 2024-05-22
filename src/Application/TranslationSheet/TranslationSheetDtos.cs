namespace ITranslateTrainer.Application.TranslationSheet;

public record ParseTranslationResponse(
    ParseTextResponse OriginText,
    ParseTextResponse TranslationText);

public record ParseTextResponse(
    string Value,
    string Language);
