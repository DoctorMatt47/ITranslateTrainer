using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.TranslationTexts;

internal record GetTranslationTextsById(int Id) : IRequest<IEnumerable<Text>>;

internal class GetTranslationTextsByIdHandler :
    IRequestHandler<GetTranslationTextsById, IEnumerable<Text>>
{
    private readonly ITranslateDbContext _context;

    public GetTranslationTextsByIdHandler(ITranslateDbContext context) => _context = context;

    public async Task<IEnumerable<Text>> Handle(
        GetTranslationTextsById request,
        CancellationToken cancellationToken)
    {
        var firstTexts = _context.Set<Translation>()
            .Where(t => t.OriginTextId == request.Id)
            .Include(t => t.TranslationText)
            .Select(t => t.TranslationText);

        var secondTexts = _context.Set<Translation>()
            .Where(t => t.TranslationTextId == request.Id)
            .Include(t => t.OriginText)
            .Select(t => t.OriginText);

        return await firstTexts.Concat(secondTexts).ToListAsync(cancellationToken);
    }
}
