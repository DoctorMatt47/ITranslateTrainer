namespace ITranslateTrainer.Application.Texts.Responses;

public record GetTextResponse(int Id, string String, string Language, bool CanBeOption, bool CanBeTested);