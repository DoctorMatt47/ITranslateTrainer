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
    : IRequest<TestResponse>;

internal class CreateTestCommandHandler : IRequestHandler<GetOrCreateTestCommand, TestResponse>
{
    private readonly ITranslateDbContext _context;

    public CreateTestCommandHandler(ITranslateDbContext context)
    {
        _context = context;
    }

    public async Task<TestResponse> Handle(
        GetOrCreateTestCommand request,
        CancellationToken cancellationToken)
    {
        var (from, to, optionCount) = request;

        var test = await _context.Set<Test>()
            .Where(Is.Not(Test.IsAnsweredExpression))
            .FirstOrDefaultAsync(t => t.OptionCount == optionCount, cancellationToken);

        if (test is not null)
        {
            return test.ToResponse();
        }

        test = await CreateTest(from, to, optionCount, cancellationToken);
        await _context.Set<Test>().AddAsync(test, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return test.ToResponse();
    }

    private async Task<Test> CreateTest(
        string from,
        string to,
        int optionCount,
        CancellationToken cancellationToken)
    {
        var text = await _context.Set<Text>()
            .Where(t => t.Language == from)
            .Shuffle()
            .FirstAsync(cancellationToken);

        var correct = text.GetTranslationTexts()
            .Select(static text => new Option
            {
                Text = text,
                IsCorrect = true,
            })
            .ToList();

        var incorrect = await _context.Set<Text>()
            .GetRandomCanBeOption(to, optionCount - correct.Count)
            .Select(static text => new Option
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
            Text = text,
            Options = options,
        };
    }
}
