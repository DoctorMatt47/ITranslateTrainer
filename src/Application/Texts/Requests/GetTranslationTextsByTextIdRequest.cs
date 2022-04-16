using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts.Requests;

public record GetTranslationTextsByTextIdRequest(uint TextId) : IRequest<IEnumerable<Text>>;

public record GetTranslationTextsByTextIdRequestHandler(ITranslateDbContext _context) :
    IRequestHandler<GetTranslationTextsByTextIdRequest, IEnumerable<Text>>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<IEnumerable<Text>> Handle(GetTranslationTextsByTextIdRequest request,
        CancellationToken cancellationToken)
    {
        var firstTexts = _context.Set<Translation>().Where(t => t.First.Id == request.TextId).Include(t => t.Second)
            .Select(t => t.Second);
        var secondTexts = _context.Set<Translation>().Where(t => t.Second.Id == request.TextId).Include(t => t.First)
            .Select(t => t.First);

        return await firstTexts.Concat(secondTexts).ToListAsync(cancellationToken);
    }
}