namespace ITranslateTrainer.Application.Tests;

public record GetOrCreateTestResponse(
    int Id,
    string String,
    IEnumerable<GetOrCreateOptionResponse> Options);

public record GetOrCreateOptionResponse(string String);
