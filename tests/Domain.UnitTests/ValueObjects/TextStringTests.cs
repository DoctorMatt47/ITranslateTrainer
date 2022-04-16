﻿using ITranslateTrainer.Domain.Exceptions;
using ITranslateTrainer.Domain.ValueObjects;
using Xunit;

namespace ITranslateTrainer.Domain.UnitTests.ValueObjects;

public class TextStringTests
{
    [Theory]
    [InlineData("1")]
    public void ThrowsDomainArgumentExceptionIfInvalidString(string invalid)
    {
        Assert.Throws<DomainArgumentException>(() => (TextString) invalid);
    }

    [Theory]
    [InlineData("Valid")]
    [InlineData("Correct")]
    public void DoesNotThrowExceptionIfValidString(string valid)
    {
        var exception = Record.Exception(() => (TextString) valid);
        Assert.Null(exception);
    }
}