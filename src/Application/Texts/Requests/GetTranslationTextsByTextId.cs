using AutoMapper;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts.Requests;

public record GetTranslationTextsByTextId(int TextId) : IRequest<IEnumerable<Text>>;

public record GetTranslationsByTextIdHandler(ITranslateDbContext _context, IMapper Mapper) :
    IRequestHandler<GetTranslationTextsByTextId, IEnumerable<Text>>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<IEnumerable<Text>> Handle(GetTranslationTextsByTextId request,
        CancellationToken cancellationToken)
    {
        var firstTexts = _context.Set<Translation>().Where(t => t.FirstId == request.TextId).Include(t => t.Second)
            .Select(t => t.Second);
        var secondTexts = _context.Set<Translation>().Where(t => t.SecondId == request.TextId).Include(t => t.First)
            .Select(t => t.First);

        return await firstTexts.Concat(secondTexts).ToListAsync(cancellationToken);
    }
}
