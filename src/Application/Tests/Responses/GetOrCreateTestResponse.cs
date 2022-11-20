namespace ITranslateTrainer.Application.Tests.Responses;

public record GetOrCreateTestResponse(
    int Id,
    string String,
    IEnumerable<GetOrCreateOptionResponse> Options);

public record GetOrCreateOptionResponse(string String);
