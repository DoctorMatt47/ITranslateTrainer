// ReSharper disable UnusedMember.Local

using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Text : EntityBase<int>
{
    private readonly string _language = null!;
    private List<Translation> _originTextTranslations = null!;
    private List<Translation> _translationTextTranslations = null!;
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

    public IEnumerable<Translation> OriginTextTranslations
    {
        get => _originTextTranslations.AsReadOnly();
        private set => _originTextTranslations = value.ToList();
    }

    public IEnumerable<Translation> TranslationTextTranslations
    {
        get => _translationTextTranslations.AsReadOnly();
        private set => _translationTextTranslations = value.ToList();
    }

    public IEnumerable<Text> GetTranslationTexts()
    {
        return OriginTextTranslations.Select(t => t.TranslationText)
            .Concat(TranslationTextTranslations.Select(t => t.OriginText));
    }
}
