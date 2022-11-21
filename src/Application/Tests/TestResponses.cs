namespace ITranslateTrainer.Application.Tests;

public record GetTestResponse(
    int Id,
    string String,
    IEnumerable<GetOptionResponse> Options);

public record GetOptionResponse(
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
