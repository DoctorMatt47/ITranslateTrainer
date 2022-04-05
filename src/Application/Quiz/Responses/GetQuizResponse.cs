using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Domain.ValueObjects;

namespace ITranslateTrainer.Application.Quiz.Responses;

public record GetQuizResponse(TextString Text, IEnumerable<OptionResponse> Options);