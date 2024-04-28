using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts;

public record GetOrCreateText(
    string Text,
    string Language)
    : IRequest<Text>;

public class GetOrCreateTextHandler(IAppDbContext context) : IRequestHandler<GetOrCreateText, Text>
{
    public async Task<Text> Handle(GetOrCreateText request, CancellationToken cancellationToken)
    {
        var text = await FindInLocalOrInDb(context);

        if (text is not null)
        {
            return text;
        }

        text = new Text
        {
            Value = request.Text,
            Language = request.Language,
        };

        await context.Set<Text>().AddAsync(text, cancellationToken);

        return text;

        // It is necessary for bulk addition to prevent duplicates.
        async Task<Text?> FindInLocalOrInDb(IAppDbContext translateDbContext)
        {
            var textsInLocal = translateDbContext.Set<Text>().Local.FirstOrDefault(
                t => t.Value == request.Text && t.Language == request.Language);

            if (textsInLocal is not null)
            {
                return textsInLocal;
            }

            return await translateDbContext.Set<Text>().FirstOrDefaultAsync(
                t => t.Value == request.Text && t.Language == request.Language,
                cancellationToken);
        }
    }
}
