// ReSharper disable RedundantDefaultMemberInitializer

using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class Text : HasId<int>
{
    private readonly List<Translation> _translations = new();

    public required string Value { get; set; }
    public required string Language { get; init; }

    public IEnumerable<Translation> Translations => _translations.AsReadOnly();

    public IEnumerable<Text> GetTranslationTexts()
    {
        return _translations
            .SelectMany(t => new[] {t.OriginText, t.TranslationText})
            .Where(t => t.Id != Id);
    }
}
