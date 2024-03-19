namespace ITranslateTrainer.Application.Texts;

public record TextRequest(
    string Value,
    string Language);

public record TextResponse(
    int Id,
    string Value,
    string Language);
