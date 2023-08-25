namespace ITranslateTrainer.Application.Tests;

public record TestResponse
{
    public required int Id { get; init; }
    public required string Text { get; init; }
    public required string AnswerTime { get; init; }
    public required IEnumerable<OptionResponse> Options { get; init; }
}

public record OptionResponse
{
    public required int Id { get; init; }
    public required string Text { get; init; }
}
