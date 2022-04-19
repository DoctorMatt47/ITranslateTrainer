using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Texts.Requests;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Requests;

public record GetOrCreateTranslationRequest(TextString FirstString, Language FirstLanguage,
    TextString SecondString, Language SecondLanguage) : IRequest<Translation>;

public record GetOrCreateTranslationRequestHandler(ITranslateDbContext _context, IMediator _mediator) :
    IRequestHandler<GetOrCreateTranslationRequest, Translation>
{
    private readonly ITranslateDbContext _context = _context;
    private readonly IMediator _mediator = _mediator;

    public async Task<Translation> Handle(GetOrCreateTranslationRequest request, CancellationToken cancellationToken)
    {
        var (firstString, firstLanguage, secondString, secondLanguage) = request;

        var firstText = await _mediator.Send(new GetOrCreateTextRequest(firstString, firstLanguage),
            cancellationToken);

        var secondText = await _mediator.Send(new GetOrCreateTextRequest(secondString, secondLanguage),
            cancellationToken);

        var translation = await _context.Set<Translation>().FirstOrDefaultAsync(
            t => t.First == firstText && t.Second == secondText
                || t.First == secondText && t.Second == firstText,
            cancellationToken);

        if (translation is not null) return translation;

        var translationToAdd = new Translation(firstText, secondText);
        await _context.Set<Translation>().AddAsync(translationToAdd, cancellationToken);

        return translationToAdd;
    }
}

public record
    GetOrCreateTranslationRequestValidateBehaviour : IPipelineBehavior<GetOrCreateTranslationRequest, Translation>
{
    public async Task<Translation> Handle(GetOrCreateTranslationRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<Translation> next)
    {
        var (_, firstLanguage, _, secondLanguage) = request;

        if (firstLanguage == secondLanguage) throw new BadRequestException("Languages are the same");

        return await next.Invoke();
    }
}
