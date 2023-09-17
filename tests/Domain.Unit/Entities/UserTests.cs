using ITranslateTrainer.Tests.Domain.Unit.Builders;

namespace ITranslateTrainer.Tests.Domain.Unit.Entities;

public class UserTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void AddTranslation_ShouldAddTranslation()
    {
        // Arrange
        var user = new UserBuilder().Build();
        var translation = new TranslationBuilder().Build();

        // Act
        user.AddTranslation(translation);

        // Assert
        user.Translations.Should().Contain(translation);
    }
}
