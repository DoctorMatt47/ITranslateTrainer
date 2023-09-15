using System.Reflection;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Tests.Domain.Unit.Entities;

public class TextTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void TranslationTexts_ShouldReturnAllTranslationTexts()
    {
        // Arrange
        var text = _faker.Text();
        var translationCount = _faker.Random.Number(10, 100);
        var translations = _faker.Make(translationCount, () => _faker.Translation());

        var translationsField = typeof(Text).GetField("_translations", BindingFlags.NonPublic | BindingFlags.Instance)!;
        translationsField.SetValue(text, translations);

        // Act
        var translationTexts = text.GetTranslationTexts().ToList();

        // Assert
        translationTexts.Should().HaveCount(translationCount * 2);
    }
}
