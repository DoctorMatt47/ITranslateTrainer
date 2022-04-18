using ITranslateTrainer.Domain.ValueObjects;

namespace ITranslateTrainer.Application.Common.Responses;

public record OptionResponse(TextString String, bool IsCorrect);