using ITranslateTrainer.Domain.ValueObjects;

namespace ITranslateTrainer.Application.Texts.Responses;

public record GetTextResponse(int Id, TextString String, string Language, bool CanBeOption, bool CanBeTested);
