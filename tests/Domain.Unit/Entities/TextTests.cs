// ReSharper disable AccessToModifiedClosure

using ITranslateTrainer.Tests.Domain.Unit.Builders;

namespace ITranslateTrainer.Tests.Domain.Unit.Entities;

public class TextTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void TranslationTexts_ShouldReturnAllTranslationTexts()
    {
        // Arrange
        var textBuilder = new TextBuilder();

        var text = textBuilder.Build();

        var translationsCount = _faker.Random.Int(0, 100);
        var translations = _faker.Make(translationsCount, () => new TranslationBuilder().WithText(text).Build());

        text = textBuilder.WithTranslations(translations).Build();

        // Act
        var translationTexts = text.GetTranslationTexts().ToList();

        // Assert
        var assertTexts = translations
            .SelectMany(t => new[] {t.TranslationText, t.OriginText})
            .Where(t => t.Id != text.Id);

        translationTexts.Should().Equal(assertTexts);
    }
}
