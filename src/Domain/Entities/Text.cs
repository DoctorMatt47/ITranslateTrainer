// ReSharper disable UnusedMember.Local

using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Text : EntityBase<int>
{
    private List<Translation> _translations = new();

    public required string Value { get; set; }
    public required string Language { get; init; }

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
