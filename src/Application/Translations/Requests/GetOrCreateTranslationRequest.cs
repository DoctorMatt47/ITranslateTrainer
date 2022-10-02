using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Texts.Requests;
using ITranslateTrainer.Application.Translations.Extensions;
using ITranslateTrainer.Domain.Entities;
using MediatR;

namespace ITranslateTrainer.Application.Translations.Requests;

public record GetOrCreateTranslationRequest(
        string FirstString,
        string FirstLanguage,
        string SecondString,
        string SecondLanguage)
    : IRequest<Translation>;

public class GetOrCreateTranslationRequestHandler : IRequestHandler<GetOrCreateTranslationRequest, Translation>
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public GetOrCreateTranslationRequestHandler(ITranslateDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Translation> Handle(GetOrCreateTranslationRequest request, CancellationToken cancellationToken)
    {
        var (firstString, firstLanguage, secondString, secondLanguage) = request;

        var firstTextRequest = new GetOrCreateTextRequest(firstString, firstLanguage);
        var firstText = await _mediator.Send(firstTextRequest, cancellationToken);

        var secondTextRequest = new GetOrCreateTextRequest(secondString, secondLanguage);
        var secondText = await _mediator.Send(secondTextRequest, cancellationToken);

        var translation = await _context.Set<Translation>().FindByTexts(firstText, secondText, cancellationToken);

        if (translation is not null) return translation;

        var translationToAdd = new Translation(firstText, secondText);
        await _context.Set<Translation>().AddAsync(translationToAdd, cancellationToken);

        return translationToAdd;
    }
}

public record GetOrCreateTranslationRequestValidateBehaviour :
    IPipelineBehavior<GetOrCreateTranslationRequest, Translation>
{
    public async Task<Translation> Handle(
        GetOrCreateTranslationRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<Translation> next)
    {
        var (_, firstLanguage, _, secondLanguage) = request;

        if (string.Equals(firstLanguage, secondLanguage, StringComparison.InvariantCultureIgnoreCase))
            throw new BadRequestException("Languages are the same");

        return await next.Invoke();
    }
}
