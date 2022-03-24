using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Domain.Enums;
using MediatR;

namespace ITranslateTrainer.Application.Languages.Requests;

public record ParseLanguageQuery(string? LanguageString) : IRequest<Language>;

public record ParseLanguageQueryHandler : IRequestHandler<ParseLanguageQuery, Language>
{
    public Task<Language> Handle(ParseLanguageQuery request, CancellationToken cancellationToken)
    {
        if (request.LanguageString is null) throw new BadRequestException("Language string is null");
        if (Enum.TryParse(request.LanguageString, out Language language)) return Task.FromResult(language);
        throw new BadRequestException("Language string cannot be parsed");
    }
}