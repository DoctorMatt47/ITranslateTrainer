using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Domain.Enums;
using MediatR;

namespace ITranslateTrainer.Application.Texts.Commands;

public record CreateTextCommand(string String, Language Language) : IRequest<IntIdResponse>;
