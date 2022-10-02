using ITranslateTrainer.Application.Texts.Responses;

namespace ITranslateTrainer.Application.Translations.Responses;

public record GetTranslationResponse(
    int Id,
    GetTextResponse First,
    GetTextResponse Second);
