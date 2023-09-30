namespace ITranslateTrainer.Application.Texts;

public record TextResponse
{
    public required int Id { get; init; }
    public required string Value { get; init; }
    public required string Language { get; init; }
}
