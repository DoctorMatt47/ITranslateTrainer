using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;

namespace ITranslateTrainer.Application.Texts.Requests;

public record CreateText(string String, Language Language) : IRequest<Text>;

public record CreateTextHandler(ITranslateDbContext _context) : IRequestHandler<CreateText, Text>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<Text> Handle(CreateText request, CancellationToken cancellationToken)
    {
        var (str, language) = request;

        var newText = new Text {String = str, Language = language};
        await _context.Set<Text>().AddAsync(newText, cancellationToken);
        return newText;
    }
}