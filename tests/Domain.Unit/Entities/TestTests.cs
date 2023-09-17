using ITranslateTrainer.Tests.Domain.Unit.Builders;

namespace ITranslateTrainer.Tests.Domain.Unit.Entities;

public class TestTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void Answer_ShouldSetOptionChosen()
    {
        // Arrange
        var test = new TestBuilder().Build();
        var randomOption = _faker.PickRandom(test.Options);

        // Act
        test.Answer(randomOption.Id);

        // Assert
        randomOption.IsChosen.Should().BeTrue();
    }

    [Fact]
    public void Answer_ShouldSetAnswerTime()
    {
        // Arrange
        var test = new TestBuilder().Build();

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
