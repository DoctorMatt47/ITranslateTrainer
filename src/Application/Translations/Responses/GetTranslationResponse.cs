using ITranslateTrainer.Application.Texts.Responses;

namespace ITranslateTrainer.Application.Translations.Responses;

public record GetTranslationResponse(uint Id, GetTextResponse First, GetTextResponse Second);