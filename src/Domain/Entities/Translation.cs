using ITranslateTrainer.Domain.Common;

namespace ITranslateTrainer.Domain.Entities;

public class Translation : IntIdEntity
{
    public int FirstId { get; set; }
    public int SecondId { get; set; }

    public Text First { get; set; } = new();
    public Text Second { get; set; } = new();
}