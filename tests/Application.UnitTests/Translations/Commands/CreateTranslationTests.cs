using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Translations.Commands;
using ITranslateTrainer.Application.Translations.Requests;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ITranslateTrainer.Application.UnitTests.Translations.Commands;

public class CreateTranslationTests
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public CreateTranslationTests(IMediator mediator, ITranslateDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [Fact]
    public async Task ShouldCreateTranslation()
    {
        var command = new CreateTranslationCommand(
            new CreateTextRequest("Get", Language.English),
            new CreateTextRequest("Получить", Language.Russian));
        var idDto = await _mediator.Send(command);
        var addedTranslation = await _context.Set<Translation>().FirstOrDefaultAsync(t => t.Id == idDto.Id);

        Assert.NotNull(addedTranslation);
        Assert.Equal("Get", addedTranslation!.First.String);
        Assert.Equal(Language.English, addedTranslation.First.Language);
        Assert.Equal("Получить", addedTranslation.Second.String);
        Assert.Equal(Language.Russian, addedTranslation.Second.Language);
    }

    [Fact]
    public async Task ShouldNotCreateTextRequestIfAlreadyExists()
    {
        var command1 = new CreateTranslationCommand(
            new CreateTextRequest("One", Language.English),
            new CreateTextRequest("1", Language.Russian));

        var command2 = new CreateTranslationCommand(
            new CreateTextRequest("One", Language.English),
            new CreateTextRequest("Один", Language.Russian));

        var idDto1 = await _mediator.Send(command1);
        var idDto2 = await _mediator.Send(command2);

        var translation1 = await _context.Set<Translation>().FirstOrDefaultAsync(t => t.Id == idDto1.Id);
        var translation2 = await _context.Set<Translation>().FirstOrDefaultAsync(t => t.Id == idDto2.Id);

        Assert.Same(translation1?.First, translation2?.First);
    }

    [Fact]
    public async Task ShouldNotCreateTranslationIfAlreadyExists()
    {
        var command1 = new CreateTranslationCommand(
            new CreateTextRequest("English", Language.English),
            new CreateTextRequest("Russian", Language.Russian));

        var command2 = new CreateTranslationCommand(
            new CreateTextRequest("English", Language.English),
            new CreateTextRequest("Russian", Language.Russian));

        var command3 = new CreateTranslationCommand(
            new CreateTextRequest("English", Language.English),
            new CreateTextRequest("Russian", Language.Russian));

        var idDto1 = await _mediator.Send(command1);
        var idDto2 = await _mediator.Send(command2);
        var idDto3 = await _mediator.Send(command3);

        Assert.Equal(idDto1, idDto2);
        Assert.Equal(idDto2, idDto3);
    }

    [Fact]
    public async Task ShouldNotCreateTranslationWithSameLanguages()
    {
        var command = new CreateTranslationCommand(
            new CreateTextRequest("Same", Language.English),
            new CreateTextRequest("Same", Language.English));

        await Assert.ThrowsAsync<BadRequestException>(() => _mediator.Send(command));
    }
}
