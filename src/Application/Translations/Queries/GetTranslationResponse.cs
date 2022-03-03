using ITranslateTrainer.Application.Texts.Queries;

namespace ITranslateTrainer.Application.Translations.Queries;

public record GetTranslationResponse(int Id, GetTextResponse First, GetTextResponse Second);
