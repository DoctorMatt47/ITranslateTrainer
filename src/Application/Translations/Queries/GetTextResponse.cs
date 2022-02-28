using ITranslateTrainer.Domain.Enums;

namespace ITranslateTrainer.Application.Translations.Queries;

public record GetTextResponse(string String, Language Language, bool CanBeOption, bool CanBeTested);