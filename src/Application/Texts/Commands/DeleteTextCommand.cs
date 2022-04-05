using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;

namespace ITranslateTrainer.Application.Texts.Commands;

public record DeleteTextCommand(TextString TextString, Language Language);
