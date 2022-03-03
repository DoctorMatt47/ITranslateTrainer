﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ITranslateTrainer.Application.Quiz.Queries;
using ITranslateTrainer.Application.TranslationSheet.Commands;
using ITranslateTrainer.Domain.Enums;
using MediatR;
using Xunit;

namespace ITranslateTrainer.Application.UnitTests.Quiz.Queries;

public class GetQuizTests
{
    private readonly IMediator _mediator;

    public GetQuizTests(IMediator mediator) => _mediator = mediator;

    [Theory]
    [InlineData(50, 3)]
    [InlineData(100, 5)]
    [InlineData(200, 10)]
    public async Task ShouldGetQuiz(int testCount, int optionCount)
    {
        await _mediator.Send(new CreateTranslationSheetCommand(File.OpenRead("Assets/TestSheet.xlsx")));
        var quiz = await _mediator.Send(new GetQuizQuery(Language.English, Language.Russian, testCount, optionCount));
        var quizList = quiz.ToList();
        Assert.Equal(testCount, quizList.Count);
        foreach (var test in quizList) Assert.Equal(optionCount, test.Options.Count());
    }
}
