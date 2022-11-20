namespace ITranslateTrainer.Application.Texts;

public record GetTextResponse(
    int Id,
    string String,
    string Language,
    bool CanBeOption,
    bool CanBeTested);
