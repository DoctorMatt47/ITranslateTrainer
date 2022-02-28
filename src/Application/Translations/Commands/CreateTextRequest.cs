using ITranslateTrainer.Domain.Enums;

namespace ITranslateTrainer.Application.Translations.Commands;

public record CreateTextRequest(string String, Language Language);
