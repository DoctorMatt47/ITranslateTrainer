using ITranslateTrainer.Domain.Enums;

namespace ITranslateTrainer.Application.Translations.Requests;

public record CreateTextRequest(string String, Language Language);