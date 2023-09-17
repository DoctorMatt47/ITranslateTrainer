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

        // Act
        var beforeAnswer = DateTime.UtcNow;
        test.Answer(_faker.PickRandom(test.Options).Id);
        var afterAnswer = DateTime.UtcNow;

        // Assert
        test.AnswerTime.Should()
            .BeIn(DateTimeKind.Utc)
            .And.BeAfter(beforeAnswer)
            .And.BeBefore(afterAnswer);
    }
}
