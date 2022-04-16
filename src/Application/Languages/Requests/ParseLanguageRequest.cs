using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Domain.Enums;
using MediatR;

namespace ITranslateTrainer.Application.Languages.Requests;

public record ParseLanguageRequest(string LanguageString) : IRequest<Language>;

public record ParseLanguageRequestHandler : IRequestHandler<ParseLanguageRequest, Language>
{
    public Task<Language> Handle(ParseLanguageRequest request, CancellationToken cancellationToken)
    {
        if (Enum.TryParse(request.LanguageString, out Language language)) return Task.FromResult(language);
        throw new BadRequestException("Language string cannot be parsed");
    }
}