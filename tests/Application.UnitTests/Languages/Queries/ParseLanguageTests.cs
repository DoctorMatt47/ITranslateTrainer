using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Languages.Handlers;
using ITranslateTrainer.Domain.Enums;
using MediatR;
using Xunit;

namespace ITranslateTrainer.Application.UnitTests.Languages.Queries;

public class ParseLanguageTests
{
    private readonly IMediator _mediator;

    public ParseLanguageTests(IMediator mediator) => _mediator = mediator;

    [Theory]
    [InlineData("English", Language.English)]
    [InlineData("Russian", Language.Russian)]
    public async Task ShouldReturnLanguageOnValidString(string validLanguageString, Language expected)
    {
        var language = await _mediator.Send(new ParseLanguage(validLanguageString));
        Assert.Equal(language, expected);
    }

    [Theory]
    [InlineData("english")]
    [InlineData("Rusian")]
    [InlineData("")]
    public async Task ShouldThrowBadRequestExceptionOnInvalidString(string invalidLanguageString)
    {
        await Assert.ThrowsAsync<BadRequestException>(() =>
            _mediator.Send(new ParseLanguage(invalidLanguageString)));
    }

    [Fact]
    public async Task ShouldThrowBadRequestExceptionOnNull()
    {
        await Assert.ThrowsAsync<BadRequestException>(() => _mediator.Send(new ParseLanguage(null)));
    }
}