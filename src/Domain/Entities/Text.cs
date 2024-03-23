// ReSharper disable UnusedMember.Local

using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Text : EntityBase<int>
{
    private readonly string _language = null!;
    private List<Translation> _translations = null!;
    private string _value = null!;

    public required string Value
    {
        get => _value;
        set => _value = value.Trim().ToLowerInvariant();
    }

    public required string Language
    {
        get => _language;
        init => _language = value.Trim().ToLowerInvariant();
    }

    public IEnumerable<Translation> Translations
    {
        get => _translations.AsReadOnly();
        private set => _translations = value.ToList();
    }

    public IEnumerable<Text> GetTranslationTexts()
    {
        return _translations
            .SelectMany(t => new[] {t.OriginText, t.TranslationText})
            .Where(t => t.Id != Id);
    }
}
