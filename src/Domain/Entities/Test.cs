﻿// ReSharper disable  UnusedAutoPropertyAccessor.Local

using System.Linq.Expressions;
using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Test : Entity<int>
{
    private readonly List<Option> _options = new();

    public static Expression<Func<Test, bool>> IsAnsweredExpression => test => test.AnswerTime != null;
    public static Func<Test, bool> IsAnsweredFunc { get; } = IsAnsweredExpression.Compile();

    public required Text Text { get; init; }

    public DateTime? AnswerTime { get; private set; }

    public int TextId { get; private init; }
    public int OptionCount { get; private init; }

    public bool IsAnswered => IsAnsweredFunc(this);

    public required IEnumerable<Option> Options
    {
        get => _options.AsReadOnly();
        init
        {
            var list = value.ToList();
            OptionCount = list.Count;
            _options.AddRange(list);
        }
    }

    public void Answer(int optionId)
    {
        var option = _options.First(o => o.Id == optionId);
        option.IsChosen = true;
        AnswerTime = DateTime.UtcNow;
    }
}
