namespace ITranslateTrainer.Domain.UnitTests.Entities;

public class UserTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void AddTranslation_ShouldAddTranslation()
    {
        // Arrange
        var user = _faker.User();
        var translation = _faker.Translation();

        // Act
        user.AddTranslation(translation);

        // Assert
        user.Translations.Should().Contain(translation);
    }
}
