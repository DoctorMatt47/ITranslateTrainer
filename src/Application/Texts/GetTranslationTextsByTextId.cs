using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts;

internal record GetTranslationTextsByTextId(int TextId) : IRequest<IEnumerable<Text>>;

internal class GetTranslationTextsByTextIdHandler :
    IRequestHandler<GetTranslationTextsByTextId, IEnumerable<Text>>
{
    private readonly ITranslateDbContext _context;

    public GetTranslationTextsByTextIdHandler(ITranslateDbContext context) => _context = context;

    public async Task<IEnumerable<Text>> Handle(
        GetTranslationTextsByTextId request,
        CancellationToken cancellationToken)
    {
        var firstTexts = _context.Set<Translation>()
            .Where(t => t.First.Id == request.TextId)
            .Include(t => t.Second)
            .Select(t => t.Second);

        var secondTexts = _context.Set<Translation>()
            .Where(t => t.Second.Id == request.TextId)
            .Include(t => t.First)
            .Select(t => t.First);

        return await firstTexts.Concat(secondTexts).ToListAsync(cancellationToken);
    }
}
