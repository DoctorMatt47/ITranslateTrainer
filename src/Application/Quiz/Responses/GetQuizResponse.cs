using ITranslateTrainer.Application.Common.Responses;

namespace ITranslateTrainer.Application.Quiz.Responses;

public record GetQuizResponse(
    string String,
    IEnumerable<OptionResponse> Options);