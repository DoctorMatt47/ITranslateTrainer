using ITranslateTrainer.Domain.Enums;

namespace ITranslateTrainer.Application.Texts.Responses;

public record GetTextResponse(int Id, string String, Language Language, bool CanBeOption, bool CanBeTested);