using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations;

public record GetTranslationsQuery : IRequest<IEnumerable<TranslationResponse>>;

internal class GetTranslationsQueryHandler : IRequestHandler<GetTranslationsQuery, IEnumerable<TranslationResponse>>
{
    private readonly ITranslateDbContext _context;

    public GetTranslationsQueryHandler(ITranslateDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TranslationResponse>> Handle(
        GetTranslationsQuery query,
        CancellationToken cancellationToken)
    {
        return await _context.Set<Translation>()
            .Include(t => t.OriginText)
            .Include(t => t.TranslationText)
            .ProjectToResponse()
            .ToListAsync(cancellationToken);
    }
}
