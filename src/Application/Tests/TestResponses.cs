namespace ITranslateTrainer.Application.Tests;

public record TestResponse
{
    public required int Id { get; init; }
    public required string String { get; init; }
    public required string AnswerTime { get; init; }
    public required IEnumerable<OptionResponse> Options { get; init; }
}

public record OptionResponse
{
    public required int Id { get; init; }
    public required string TranslationText { get; init; }
    public required bool IsChosen { get; init; }
    public required bool IsCorrect { get; init; }
}

public record GetOrCreateTestResponse
{
    public required int Id { get; init; }
    public required string String { get; init; }
    public required IEnumerable<GetOrCreateOptionResponse> Options { get; init; }
}

public record GetOrCreateOptionResponse
{
    public required int Id { get; init; }
    public required string String { get; init; }
}
