using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts.Commands;

public record GetOrCreateText(
        string String,
        string Language)
    : IRequest<Text>;

internal class GetOrCreateTextHandler : IRequestHandler<GetOrCreateText, Text>
{
    private readonly ITranslateDbContext _context;

    public GetOrCreateTextHandler(ITranslateDbContext context) => _context = context;

    public async Task<Text> Handle(GetOrCreateText request, CancellationToken cancellationToken)
    {
        var (textString, language) = request;

        var text = await FindInLocalOrInDb(_context.Set<Text>(), textString, language, cancellationToken);

        if (text is not null) return text;

        var newText = new Text(textString, language);
        await _context.Set<Text>().AddAsync(newText, cancellationToken);

        return newText;
    }

    // Tries to find in local, if not, requests database.
    // It is necessary for bulk addition to prevent duplicates.
    private static async Task<Text?> FindInLocalOrInDb(
        DbSet<Text> texts,
        string textString,
        string language,
        CancellationToken cancellationToken)
    {
        var textsInLocal = texts.Local.FirstOrDefault(t =>
            t.String == textString.ToLowerInvariant() && t.Language == language.ToLowerInvariant());

        if (textsInLocal is not null) return textsInLocal;

        return await texts.FirstOrDefaultAsync(
            t => t.String == textString.ToLowerInvariant() && t.Language == language.ToLowerInvariant(),
            cancellationToken);
    }
}
