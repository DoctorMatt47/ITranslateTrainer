using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Texts.Requests;

public record GetRandomCanBeTestedTexts(int Count, Language Language) : IRequest<IEnumerable<Text>>;

public record GetRandomCanBeTestedTextsHandler(ITranslateDbContext _context) :
    IRequestHandler<GetRandomCanBeTestedTexts, IEnumerable<Text>>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<IEnumerable<Text>> Handle(GetRandomCanBeTestedTexts request,
        CancellationToken cancellationToken) =>
        await _context.Set<Text>().Where(t => t.CanBeTested && t.Language == request.Language).Shuffle()
            .Take(request.Count)
            .ToListAsync(cancellationToken);
}
