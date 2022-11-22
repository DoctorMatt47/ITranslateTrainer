using System.Linq.Expressions;
using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.Entities;

public class Test : IHasId<int>
{
    public Test(int textId, int optionCount)
    {
        TextId = textId;
        OptionCount = optionCount;
    }

    public static Expression<Func<Test, bool>> IsAnswered => test => test.AnswerTime != null;
    public static Expression<Func<Test, bool>> IsNotAnswered => test => test.AnswerTime == null;

    public int TextId { get; protected set; }
    public Text Text { get; protected set; } = null!;

    public int OptionCount { get; protected set; }
    public List<Option> Options { get; protected set; } = new();

    public DateTime? AnswerTime { get; protected set; }

    public int Id { get; protected set; }

    public void GotAnswer()
    {
        AnswerTime = DateTime.UtcNow;
    }
}
