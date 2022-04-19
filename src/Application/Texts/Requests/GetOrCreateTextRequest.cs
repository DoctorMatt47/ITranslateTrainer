using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts.Requests;

public record GetOrCreateTextRequest(TextString String, Language Language) : IRequest<Text>;

public record GetOrCreateTextRequestHandler(ITranslateDbContext _context) :
    IRequestHandler<GetOrCreateTextRequest, Text>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<Text> Handle(GetOrCreateTextRequest request, CancellationToken cancellationToken)
    {
        var (textString, language) = request;

        var texts = _context.Set<Text>();

        var text = texts.Local.FirstOrDefault(t => t.String == textString && t.Language == language) ??
            await texts.FirstOrDefaultAsync(t => t.String == textString && t.Language == language, cancellationToken);

        if (text is not null) return text;

        var newText = new Text(textString, language);
        await texts.AddAsync(newText, cancellationToken);

        return newText;
    }
}
