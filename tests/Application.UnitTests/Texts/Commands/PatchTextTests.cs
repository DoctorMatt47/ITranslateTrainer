using System.Threading.Tasks;
using ITranslateTrainer.Application.Texts.Commands;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ITranslateTrainer.Application.UnitTests.Texts.Commands;

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
    public async Task ShouldPatchText()
    {
        var textToAdd = new Text
        {
            Language = Language.English,
            String = "patch test"
        };
        await _context.Set<Text>().AddAsync(textToAdd);
        await _context.SaveChangesAsync();

        await _mediator.Send(new PatchTextCommand(textToAdd.Id, false, false));
        var text = await _context.Set<Text>().FirstAsync(t => t.Id == textToAdd.Id);
        Assert.False(text.CanBeOption);
        Assert.False(text.CanBeTested);
    }
}