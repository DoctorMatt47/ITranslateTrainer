namespace ITranslateTrainer.Application.Tests;

public record TestResponse(
    int Id,
    string String,
    string AnswerTime,
    IEnumerable<OptionResponse> Options);

public record OptionResponse(
    int Id,
    string String,
    bool IsChosen,
    bool IsCorrect);

public record GetOrCreateTestResponse(
    int Id,
    string String,
    IEnumerable<GetOrCreateOptionResponse> Options);

public record GetOrCreateOptionResponse(
    int Id,
    string String);
