using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.Entities;

public class Test : IHasId<int>
{
    public Test(int textId, int optionCount)
    {
        TextId = textId;
        OptionCount = optionCount;
    }

    public int TextId { get; protected set; }
    public Text Text { get; protected set; } = null!;

    public int OptionCount { get; protected set; }
    public List<Option> Options { get; protected set; } = new();

    public bool IsAnswered { get; set; }

    public int Id { get; protected set; }
}
