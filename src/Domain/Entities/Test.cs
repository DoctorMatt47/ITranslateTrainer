// ReSharper disable  UnusedAutoPropertyAccessor.Local

using System.Linq.Expressions;
using ITranslateTrainer.Domain.Abstractions;
using ITranslateTrainer.Domain.Exceptions;

namespace ITranslateTrainer.Domain.Entities;

public class Test : EntityBase<int>
{
    private readonly List<Option> _options = new();

    public required Text Text { get; init; }

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

    public DateTimeOffset? AnswerTime { get; private set; }
    public int TextId { get; private init; }
    public int OptionCount { get; private init; }
    public bool IsAnswered => IsAnsweredFunc(this);

    public static Expression<Func<Test, bool>> IsAnsweredExpression => test => test.AnswerTime != null;
    public static Func<Test, bool> IsAnsweredFunc { get; } = IsAnsweredExpression.Compile();

    public void Answer(int optionId)
    {
        var option = _options.FirstOrDefault(o => o.Id == optionId);
        if (option is null) throw NotFoundException.DoesNotExist<Option>(optionId);

        option.IsChosen = true;
        AnswerTime = DateTimeOffset.UtcNow;
    }
}
