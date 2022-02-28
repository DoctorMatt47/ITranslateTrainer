namespace ITranslateTrainer.Application.Translations.Queries;

public record GetTranslationResponse(GetTextResponse First, GetTextResponse Second);