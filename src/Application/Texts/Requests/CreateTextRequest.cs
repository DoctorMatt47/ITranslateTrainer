using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using MediatR;

namespace ITranslateTrainer.Application.Texts.Requests;

public record CreateTextRequest(string String, Language Language) : IRequest<Text>;

public record CreateTextRequestHandler(ITranslateDbContext _context) : IRequestHandler<CreateTextRequest, Text>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<Text> Handle(CreateTextRequest request, CancellationToken cancellationToken)
    {
        var (str, language) = request;

        var newText = new Text(str, language);
        await _context.Set<Text>().AddAsync(newText, cancellationToken);

        return newText;
    }
}