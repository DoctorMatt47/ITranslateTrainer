using ITranslateTrainer.Domain.Attributes;
using Xunit;

namespace ITranslateTrainer.Domain.UnitTests.Attributes;

public class TextStringAttributeTests
{
    private static readonly TextStringAttribute Attr = new();

    [Theory]
    [InlineData("Valid")]
    [InlineData("Correct")]
    public void TrueIfValid(string valid)
    {
        Assert.True(Attr.IsValid(valid));
    }

    [Fact]
    public void FalseIfNull()
    {
        Assert.False(Attr.IsValid(null));
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    public void FalseIfLengthLessThan2(string invalid)
    {
        Assert.False(Attr.IsValid(invalid));
    }

    [Theory]
    [InlineData("Lorem ipsum dolor sit amet, Lorem ipsum dolor amet..")]
    [InlineData("Lorem ipsum dolor sit amet, amet.. Lorem ipsum dolor sit amet dolor sit amet")]
    public void FalseIfLengthMoreThan50(string invalid)
    {
        Assert.False(Attr.IsValid(invalid));
    }

    [Theory]
    [InlineData(5)]
    [InlineData('c')]
    public void FalseIfNotString(object notString)
    {
        Assert.False(Attr.IsValid(5));
    }
}