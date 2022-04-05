using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;

namespace ITranslateTrainer.Domain.Entities;

public class Text : UintIdBase
{
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly List<Translation> _translations = new();

    public Text(TextString textString, Language language)
    {
        String = textString;
        Language = language;
    }

    public TextString String { get; protected set; }
    public Language Language { get; protected set; }

    public bool CanBeOption { get; set; } = true;
    public bool CanBeTested { get; set; } = true;
    public IEnumerable<Translation> Translations => _translations.ToList();
}