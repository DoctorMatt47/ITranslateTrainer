using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts.Requests;

public record GetRandomCanBeOptionTexts(int Count, Language Language) : IRequest<IEnumerable<Text>>;

public record GetRandomCanBeOptionTextsHandler(ITranslateDbContext _context) :
    IRequestHandler<GetRandomCanBeOptionTexts, IEnumerable<Text>>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<IEnumerable<Text>> Handle(GetRandomCanBeOptionTexts request,
        CancellationToken cancellationToken) =>
        await _context.Set<Text>().Where(t => t.CanBeOption && t.Language == request.Language).Shuffle()
            .Take(request.Count)
            .ToListAsync(cancellationToken);
}
