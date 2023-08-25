using AutoMapper;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.TranslationTexts;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record GetOrCreateTestCommand(
        string From,
        string To,
        int OptionCount)
    : IRequest<GetOrCreateTestResponse>;

internal class CreateTestCommandHandler : IRequestHandler<GetOrCreateTestCommand, GetOrCreateTestResponse>
{
    private readonly ITranslateDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateTestCommandHandler(ITranslateDbContext context, IMediator mediator, IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<GetOrCreateTestResponse> Handle(
        GetOrCreateTestCommand request,
        CancellationToken cancellationToken)
    {
        var (from, to, optionCount) = request;

        var test = await _context.Set<Test>()
            .Where(Is.Not(Test.IsAnsweredExpression))
            .FirstOrDefaultAsync(t => t.OptionCount == optionCount, cancellationToken);

        if (test is null)
        {
            return _mapper.Map<GetOrCreateTestResponse>(test);
        }

        test = await CreateTest(from, to, optionCount, cancellationToken);
        await _context.Set<Test>().AddAsync(test, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetOrCreateTestResponse>(test);
    }

    private async Task<Test> CreateTest(
        string from,
        string to,
        int optionCount,
        CancellationToken cancellationToken)
    {
        var translationText = await _context.Set<Text>()
            .Where(t => t.Language == from)
            .Shuffle()
            .FirstAsync(cancellationToken);

        var correct = (await _mediator.Send(new GetTranslationTextsById(translationText.Id), cancellationToken))
            .Select(text => new Option
            {
                Text = text,
                IsCorrect = true,
            })
            .ToList();

        var incorrect = await _context.Set<Text>()
            .GetRandomCanBeOption(to, optionCount - correct.Count)
            .Select(text => new Option
            {
                Text = text,
                IsCorrect = false,
            })
            .ToListAsync(cancellationToken);

        var options = correct
            .Concat(incorrect)
            .Shuffle()
            .ToList();

        return new Test
        {
            Text = translationText,
            Options = options,
        };
    }
}
