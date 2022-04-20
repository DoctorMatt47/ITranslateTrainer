using ITranslateTrainer.Domain.Exceptions;
using ITranslateTrainer.Domain.ValueObjects;
using Xunit;

namespace ITranslateTrainer.Domain.UnitTests.ValueObjects;

public class TextStringTests
{
    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData(null)]
    public void ThrowsDomainArgumentExceptionIfInvalidString(string? invalid)
    {
        Assert.Throws<DomainArgumentException>(() => TextString.From(invalid!));
    }

    [Theory]
    [InlineData("Valid")]
    [InlineData("Correct")]
    public void DoesNotThrowExceptionIfValidString(string valid)
    {
        var exception = Record.Exception(() => TextString.From(valid));
        Assert.Null(exception);
    }
}
