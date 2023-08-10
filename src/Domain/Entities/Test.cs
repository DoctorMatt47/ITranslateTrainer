using System.Linq.Expressions;
using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.Entities;

public class Test : IHasId<int>
{
    public Test(int translationTextId, int optionCount)
    {
        TranslationTextId = translationTextId;
        OptionCount = optionCount;
    }

    public static Expression<Func<Test, bool>> IsAnswered => test => test.AnswerTime != null;
    public static Expression<Func<Test, bool>> IsNotAnswered => test => test.AnswerTime == null;

    public int TranslationTextId { get; protected set; }
    public TranslationText TranslationText { get; protected set; } = null!;

    public int OptionCount { get; protected set; }
    public List<Option> Options { get; protected set; } = new();

    public DateTime? AnswerTime { get; protected set; }

    public int Id { get; protected set; }

    public void Answer()
    {
        AnswerTime = DateTime.UtcNow;
    }
}
