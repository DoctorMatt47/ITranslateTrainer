using ITranslateTrainer.Application.Texts;

namespace ITranslateTrainer.Application.Translations;

public record GetTranslationResponse(
    int Id,
    GetTextResponse First,
    GetTextResponse Second);
