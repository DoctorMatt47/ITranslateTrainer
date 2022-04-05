using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Texts.Handlers;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Handlers;

public record CreateTranslation(TextString FirstString, Language FirstLanguage,
    TextString SecondString, Language SecondLanguage) : IRequest<Translation>;

public record CreateTranslationHandler(ITranslateDbContext _context, IMediator _mediator) :
    IRequestHandler<CreateTranslation, Translation>
{
    private readonly ITranslateDbContext _context = _context;
    private readonly IMediator _mediator = _mediator;

    public async Task<Translation> Handle(CreateTranslation request,
        CancellationToken cancellationToken)
    {
        var (firstString, firstLanguage, secondString, secondLanguage) = request;

        var firstText = await _context.Set<Text>().FirstOrDefaultAsync(
            t => t.String == firstString && t.Language == firstLanguage,
            cancellationToken);

        var secondText = await _context.Set<Text>().FirstOrDefaultAsync(
            t => t.String == secondString && t.Language == secondLanguage,
            cancellationToken);

        if (firstText is not null && secondText is not null)
        {
            var translation = await _context.Set<Translation>().FirstOrDefaultAsync(
                t => t.First == firstText && t.Second == secondText
                    || t.Second == secondText && t.First == firstText,
                cancellationToken);
            if (translation is not null) return translation;
        }

        firstText ??= await _mediator.Send(new CreateText(firstString, firstLanguage), cancellationToken);
        secondText ??= await _mediator.Send(new CreateText(secondString, secondLanguage), cancellationToken);

        var translationToAdd = new Translation(firstText, secondText);
        await _context.Set<Translation>().AddAsync(translationToAdd, cancellationToken);

        return translationToAdd;
    }
}

public record CreateTranslationValidateBehaviour : IPipelineBehavior<CreateTranslation, Translation>
{
    public async Task<Translation> Handle(CreateTranslation request, CancellationToken cancellationToken,
        RequestHandlerDelegate<Translation> next)
    {
        var (_, firstLanguage, _, secondLanguage) = request;

        if (firstLanguage == secondLanguage) throw new BadRequestException("Languages are the same");

        return await next.Invoke();
    }
}