using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Texts.Commands;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;
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
    public async Task ShouldChangeBoolOnFalseText()
    {
        var testedText = new Text(TextString.From("patchTest"), Language.English);
        _context.Set<Text>().Add(testedText);
        await _context.SaveChangesAsync();

        var command = new PatchTextCommand(testedText.Id, false, false);
        await _mediator.Send(command);

        var resultText = await _context.Set<Text>().FindAsync(testedText.Id);

        Assert.False(resultText!.CanBeOption);
        Assert.False(resultText.CanBeTested);
    }
}