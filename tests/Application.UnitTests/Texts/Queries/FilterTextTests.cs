using System.Threading.Tasks;
using ITranslateTrainer.Application.Texts.Queries;
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
        var filtered = await _mediator.Send(new FilterTextQuery(toFilter));
        Assert.Equal(expected, filtered);
    }
}
