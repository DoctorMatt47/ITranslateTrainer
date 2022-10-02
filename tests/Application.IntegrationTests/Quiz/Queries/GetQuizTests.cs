using System.Linq;
using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Quiz.Queries;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Xunit;

namespace ITranslateTrainer.Application.IntegrationTests.Quiz.Queries;

public class GetQuizTests
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public GetQuizTests(IMediator mediator, ITranslateDbContext context)
    {
        _mediator = mediator;
        _context = context;

        FillDatabase();
    }

    private void FillDatabase()
    {
        var texts = _context.Set<Text>();
        var translations = _context.Set<Translation>();
        for (var i = 0; i < 200; i++)
        {
            var eng = new Text($"{i}-{i}", "English");
            var rus = new Text($"{i}-{i}", "Russian");
            texts.AddRange(eng, rus);
            translations.Add(new Translation(eng, rus));
        }

        _context.SaveChangesAsync().GetAwaiter().GetResult();
    }

    [Theory]
    [InlineData(50, 3)]
    [InlineData(100, 5)]
    [InlineData(200, 10)]
    public async Task ShouldContainCorrectOption(int testCount, int optionCount)
    {
        var quiz = await _mediator.Send(new GetQuizQuery("English", "Russian", testCount, optionCount));
        var quizList = quiz.ToList();

        Assert.Equal(testCount, quizList.Count);

        foreach (var test in quizList) Assert.Equal(optionCount, test.Options.Count());
    }
}