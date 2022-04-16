using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Texts.Commands;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using MediatR;
using Xunit;

namespace ITranslateTrainer.Application.IntegrationTests.Texts.Commands;

public class PatchTextTests
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public PatchTextTests(IMediator mediator, ITranslateDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [Fact]
    public async Task ShouldChangedBoolOnFalseText()
    {
        var testedText = new Text("patch test", Language.English);
        await _context.Set<Text>().AddAsync(testedText);

        var command = new PatchTextCommand(testedText.Id, false, false);
        await _mediator.Send(command);

        Assert.False(testedText.CanBeOption);
        Assert.False(testedText.CanBeTested);
    }
}
