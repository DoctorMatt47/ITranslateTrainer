using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record GetOrCreateTestCommand(string FromLanguage, string ToLanguage, int OptionCount) : IRequest<TestResponse>;

public class CreateTestCommandHandler(IAppDbContext context) : IRequestHandler<GetOrCreateTestCommand, TestResponse>
{
    private GetOrCreateTestCommand _request = null!;

    public async Task<TestResponse> Handle(
        GetOrCreateTestCommand request,
        CancellationToken cancellationToken)
    {
        _request = request;

        var test = await context.Set<Test>()
            .Where(t => t.OptionCount == _request.OptionCount)
            .Where(Is.Not(Test.IsAnsweredExpression))
            .FirstOrDefaultAsync(cancellationToken);

        if (test is not null) return test.ToResponse();

        test = await CreateRandomTest(cancellationToken);

        await context.Set<Test>().AddAsync(test, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return test.ToResponse();
    }

    private async Task<Test> CreateRandomTest(CancellationToken cancellationToken)
    {
        var text = await context.Set<Text>()
            .Where(t => t.Language == _request.FromLanguage)
            .Shuffle()
            .FirstOrDefaultAsync(cancellationToken);

        if (text is null) throw new NotFoundException("There is not any text in this language");

        var options = await CreateRandomOptions(text, cancellationToken);

        return new()
        {
            Text = text,
            Options = options,
        };
    }

    private async Task<List<Option>> CreateRandomOptions(Text text, CancellationToken cancellationToken)
    {
        var correctOptions = text.GetTranslationTexts()
            .Select(t => new Option
            {
                Text = t,
                IsCorrect = true,
            })
            .ToList();

        var incorrectOptionCount = _request.OptionCount - correctOptions.Count;

        var incorrectOptions = await context.Set<Text>()
            .Where(t => t.Language == _request.ToLanguage)
            .Shuffle()
            .Take(incorrectOptionCount)
            .Select(t => new Option
            {
                Text = t,
                IsCorrect = false,
            })
            .ToListAsync(cancellationToken);

        return correctOptions
            .Concat(incorrectOptions)
            .Shuffle()
            .ToList();
    }
}
