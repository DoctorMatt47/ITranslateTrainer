using System.Threading;
using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Languages.Requests;
using ITranslateTrainer.Domain.Enums;
using Xunit;

namespace ITranslateTrainer.Application.UnitTests.Languages.Queries;

public class ParseLanguageTests
{
    private static readonly ParseLanguageRequestHandler RequestHandler = new();
    private static readonly CancellationToken CancellationToken = CancellationToken.None;

    [Theory]
    [InlineData("English", Language.English)]
    [InlineData("Russian", Language.Russian)]
    public async Task ShouldReturnLanguageOnValidString(string validLanguageString, Language expected)
    {
        var request = new ParseLanguageRequest(validLanguageString);
        var language = await RequestHandler.Handle(request, CancellationToken);
        Assert.Equal(language, expected);
    }

    [Theory]
    [InlineData("english")]
    [InlineData("Rusian")]
    [InlineData("")]
    public async Task ShouldThrowBadRequestExceptionOnInvalidString(string invalidLanguageString)
    {
        var request = new ParseLanguageRequest(invalidLanguageString);
        await Assert.ThrowsAsync<BadRequestException>(() => RequestHandler.Handle(request, CancellationToken));
    }
}