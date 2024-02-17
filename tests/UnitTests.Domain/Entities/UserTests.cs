﻿using ITranslateTrainer.Domain.Entities;
using Xunit.Abstractions;

namespace ITranslateTrainer.UnitTests.Domain.Entities;

public class UserTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ThrowException()
    {
        testOutputHelper.WriteLine("Id");
        testOutputHelper.WriteLine(nameof(MegaTest.Identifier));
    }

    [Fact]
    public void AddTranslation_AddOneTranslation_AddsTranslation()
    {
        // Arrange
        var user = new Faker<User>().Generate();
        var translation = new Faker<Translation>().Generate();

        // Act
        user.AddTranslation(translation);

        // Assert
        user.Translations.Should().Contain(translation);
    }

    [Fact]
    public void AddTranslation_AddTwoTranslations_AddsTwoTranslation()
    {
        // Arrange
        var user = new Faker<User>().Generate();
        var translation1 = new Faker<Translation>().Generate();
        var translation2 = new Faker<Translation>().Generate();

        // Act
        user.AddTranslation(translation1);
        user.AddTranslation(translation2);

        // Assert
        user.Translations.Should()
            .Contain(translation1)
            .And.Contain(translation2);
    }

    internal class MegaTest
    {
        public int Identifier;
    }
}
