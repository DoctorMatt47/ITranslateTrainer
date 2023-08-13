using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Translations;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ITranslateTrainer.Application.IntegrationTests.Translations.Commands;

public class PutTranslationTests
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public PutTranslationTests(IMediator mediator, ITranslateDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [Fact]
    public async Task ShouldCreateTranslation()
    {
        var command = new PutTranslationCommand("get", "English", "получить", "Russian");

        var idDto = await _mediator.Send(command);
        var addedTranslation = await _context.Set<Translation>().FirstOrDefaultAsync(t => t.Id == idDto.Id);

        Assert.NotNull(addedTranslation);
        Assert.Equal("get", addedTranslation!.First.Text);
        Assert.Equal("english", addedTranslation.First.Language);
        Assert.Equal("получить", addedTranslation.Second.Text);
        Assert.Equal("russian", addedTranslation.Second.Language);
    }

    [Fact]
    public async Task ShouldNotCreateTextRequestIfAlreadyExists()
    {
        var command1 = new PutTranslationCommand("One", "English", "111", "Russian");

        var command2 = new PutTranslationCommand("One", "English", "Один", "Russian");

        var idDto1 = await _mediator.Send(command1);
        var idDto2 = await _mediator.Send(command2);

        var translation1 = await _context.Set<Translation>().FirstOrDefaultAsync(t => t.Id == idDto1.Id);
        var translation2 = await _context.Set<Translation>().FirstOrDefaultAsync(t => t.Id == idDto2.Id);

        Assert.Same(translation1?.First, translation2?.First);
    }

    [Fact]
    public async Task ShouldNotCreateTranslationIfAlreadyExists()
    {
        var command1 = new PutTranslationCommand("English", "English", "Russian", "Russian");

        var command2 = new PutTranslationCommand("English", "English", "Russian", "Russian");

        var command3 = new PutTranslationCommand("English", "English", "Russian", "Russian");

        var idDto1 = await _mediator.Send(command1);
        var idDto2 = await _mediator.Send(command2);
        var idDto3 = await _mediator.Send(command3);

        Assert.Equal(idDto1, idDto2);
        Assert.Equal(idDto2, idDto3);
    }

    [Fact]
    public async Task ShouldNotCreateTranslationWithSameLanguages()
    {
        var command = new PutTranslationCommand("Same", "English", "Same", "English");

        await Assert.ThrowsAsync<BadRequestException>(() => _mediator.Send(command));
    }
}
