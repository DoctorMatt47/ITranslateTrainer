using ITranslateTrainer.Application.Texts.Queries;

namespace ITranslateTrainer.Application.Translations.Queries;

public record GetTranslationResponse(GetTextResponse First, GetTextResponse Second);