using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;

namespace ITranslateTrainer.Domain.Entities;

public class Text : UintIdBase
{
    protected Text()
    {
    }

    public Text(TextString @string, Language language)
    {
        String = @string;
        Language = language;
    }

    public TextString String { get; protected set; } = null!;
    public Language Language { get; protected set; }

    public bool CanBeOption { get; set; } = true;
    public bool CanBeTested { get; set; } = true;
}
