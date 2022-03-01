using ITranslateTrainer.Domain.Common;
using ITranslateTrainer.Domain.Enums;

namespace ITranslateTrainer.Domain.Entities;

public class Text : IntIdEntity
{
    public string String { get; set; } = string.Empty;
    public Language Language { get; set; }
    public bool CanBeOption { get; set; } = true;
    public bool CanBeTested { get; set; } = true;
}