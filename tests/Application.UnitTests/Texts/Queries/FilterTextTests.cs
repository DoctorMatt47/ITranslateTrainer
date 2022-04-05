using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Texts.Extensions;
using MediatR;
using Xunit;

namespace ITranslateTrainer.Application.UnitTests.Texts.Queries;

public class FilterTextTests
{
    private readonly IMediator _mediator;

    public FilterTextTests(IMediator mediator) => _mediator = mediator;

    [Theory]
    [InlineData("Test\n", "Test")]
    [InlineData("Test\t", "Test")]
    [InlineData("Test ", "Test")]
    public async Task ShouldFilterString(string toFilter, string expected)
    {
        var filtered = await _mediator.Send(new FilterText(toFilter));
        Assert.Equal(expected, filtered);
    }

    [Fact]
    public async Task ShouldThrowException()
    {
        await Assert.ThrowsAsync<BadRequestException>(() => _mediator.Send(new FilterText(null)));
    }
}
