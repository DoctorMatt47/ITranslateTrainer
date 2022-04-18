using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;

namespace ITranslateTrainer.Application.Texts.Responses;

public record GetTextResponse(uint Id, TextString String, Language Language, bool CanBeOption, bool CanBeTested);