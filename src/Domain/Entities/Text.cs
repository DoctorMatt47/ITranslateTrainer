using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.Exceptions;
using ITranslateTrainer.Domain.ValueObjects;

namespace ITranslateTrainer.Domain.Entities;

public class Text : IntIdBase
{
    protected Text()
    {
    }

    public Text(TextString @string, Language language)
    {
        if (language == Language.None)
            throw new DomainArgumentException("Language should not be none", nameof(language));

        String = @string;
        Language = language;
    }

    public TextString String { get; protected set; } = null!;
    public Language Language { get; protected set; }

    public bool CanBeOption { get; set; } = true;
    public bool CanBeTested { get; set; } = true;
}