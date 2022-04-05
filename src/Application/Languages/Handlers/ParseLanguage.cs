using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Domain.Enums;
using MediatR;

namespace ITranslateTrainer.Application.Languages.Handlers;

public record ParseLanguage(string? LanguageString) : IRequest<Language>;

public record ParseLanguageHandler : IRequestHandler<ParseLanguage, Language>
{
    public Task<Language> Handle(ParseLanguage request, CancellationToken cancellationToken)
    {
        if (request.LanguageString is null) throw new BadRequestException("Language string is null");
        if (Enum.TryParse(request.LanguageString, out Language language)) return Task.FromResult(language);
        throw new BadRequestException("Language string cannot be parsed");
    }
}