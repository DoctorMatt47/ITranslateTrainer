using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Tests.Domain.Unit.Entities;

public class TestTests
{
    private readonly Faker _faker = new();

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void Answer_HasOptions_OptionWithPassedIdIsChosen(int optionCount)
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
        test.Answer(randomOption.Id);

        // Assert
        randomOption.IsChosen.Should().BeTrue();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void Answer_ShouldSetAnswerTime(int optionCount)
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
        test.Answer(_faker.PickRandom(test.Options).Id);

        // Assert
        var afterAnswer = DateTime.UtcNow;

        test.AnswerTime.Should()
            .BeAfter(beforeAnswer)
            .And.BeBefore(afterAnswer);
    }
}
