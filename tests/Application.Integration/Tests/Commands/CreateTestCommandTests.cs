using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using MediatR;
using Xunit;

namespace ITranslateTrainer.Tests.Application.Integration.Tests.Commands;

public class CreateTestCommandTests
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public CreateTestCommandTests(IMediator mediator, ITranslateDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [Fact]
    public async Task ShouldCreateTest()
    {
    }
}
