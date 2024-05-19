using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.UnitTests.Domain.Entities;

public class TextTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void GetTranslationTexts_NoTranslations_ReturnsEmpty()
    {
        // Arrange
        var text = new Faker<Text>()
            .RuleFor(t => t.Id, faker => faker.Random.Int())
            .RuleFor(t => t.TranslationTextTranslations, _ => new List<Translation>())
            .Generate();

        // Act
        var translationTexts = text.GetTranslationTexts();

        // Assert
        translationTexts.Should().BeEmpty();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void GetTranslationTexts_TranslationsWithOriginTextEqualsSut_HaveSameCount(int translationCount)
    {
        // Arrange
        var textId = _faker.Random.Int();
        var translations = MakeTranslationsWithOriginTextId(translationCount, textId);

        var text = new Faker<Text>()
            .RuleFor(t => t.Id, _ => textId)
            .RuleFor(t => t.TranslationTextTranslations, _ => translations)
            .Generate();

        // Act
        var translationTexts = text.GetTranslationTexts().ToList();

        // Assert
        translationTexts.Should().HaveCount(translationCount);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void GetTranslationTexts_TranslationsWithTranslationTextEqualsSut_HaveSameCount(int translationCount)
    {
        // Arrange
        var textId = _faker.Random.Int();
        var translations = MakeTranslationsWithTranslationTextId(translationCount, textId);

        var text = new Faker<Text>()
            .RuleFor(t => t.Id, _ => textId)
            .RuleFor(t => t.TranslationTextTranslations, _ => translations)
            .Generate();

        // Act
        var translationTexts = text.GetTranslationTexts().ToList();

        // Assert
        translationTexts.Should().HaveCount(translationCount);
    }

    private static List<Translation> MakeTranslationsWithOriginTextId(int count, int textId)
    {
        return new Faker<Translation>()
            .RuleFor(t => t.OriginText, _ => new Faker<Text>().RuleFor(t => t.Id, _ => textId).Generate())
            .RuleFor(t => t.TranslationText, _ => new Faker<Text>().Generate())
            .Generate(count);
    }

    private static List<Translation> MakeTranslationsWithTranslationTextId(int count, int textId)
    {
        return new Faker<Translation>()
            .RuleFor(t => t.OriginText, _ => new Faker<Text>().Generate())
            .RuleFor(t => t.TranslationText, _ => new Faker<Text>().RuleFor(t => t.Id, _ => textId).Generate())
            .Generate(count);
    }
}
