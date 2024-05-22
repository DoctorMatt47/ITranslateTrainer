using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.UnitTests.Domain.Entities;

public class TestTests
{
    private readonly Faker _faker = new();

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void OptionCount_WhenInitialized_EqualToOptionsCount(int optionCount)
    {
        // Arrange
        var options = new Faker<Option>()
            .RuleFor(o => o.Id, faker => faker.Random.Int())
            .Generate(optionCount);

        // Act
        var test = new Test
        {
            Options = options,
            Text = new Faker<Text>().Generate(),
        };

        // Assert
        test.OptionCount.Should().Be(options.Count);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void SetAnswer_WhenHasOptions_SetSelectedOptionIsChosenToTrue(int optionCount)
    {
        // Arrange
        var options = new Faker<Option>()
            .RuleFor(o => o.Id, faker => faker.Random.Int())
            .Generate(optionCount);

        var test = new Faker<Test>()
            .RuleFor(t => t.Options, _ => options)
            .Generate();

        var randomOption = _faker.PickRandom(test.Options);

        // Act
        test.SetAnswer(randomOption.Id);

        // Assert
        randomOption.IsChosen.Should().BeTrue();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void SetAnswer_ShouldSetAnswerTime(int optionCount)
    {
        // Arrange
        var options = new Faker<Option>()
            .RuleFor(o => o.Id, faker => faker.Random.Int())
            .Generate(optionCount);

        var test = new Faker<Test>()
            .RuleFor(t => t.Options, _ => options)
            .Generate();

        var beforeAnswer = DateTime.UtcNow;

        // Act
        test.SetAnswer(_faker.PickRandom(test.Options).Id);

        // Assert
        var afterAnswer = DateTime.UtcNow;

        test.AnswerTime.Should()
            .BeOnOrAfter(beforeAnswer)
            .And.BeOnOrBefore(afterAnswer);
    }
}
