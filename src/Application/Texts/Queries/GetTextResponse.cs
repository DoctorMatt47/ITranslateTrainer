using ITranslateTrainer.Domain.Enums;

namespace ITranslateTrainer.Application.Texts.Queries;

public record GetTextResponse(int Id, string String, Language Language, bool CanBeOption, bool CanBeTested);
