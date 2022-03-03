using ITranslateTrainer.Application.Common.Responses;

namespace ITranslateTrainer.Application.Quiz.Queries;

public record GetQuizResponse(string Text, IEnumerable<OptionResponse> Options);
