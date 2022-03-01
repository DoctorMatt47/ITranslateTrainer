using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Translations.Commands;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.Interfaces;
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
        var addedTranslation = await _context.Translations.FirstOrDefaultAsync(t => t.Id == idDto.Id);

        Assert.NotNull(addedTranslation);
        Assert.Equal(addedTranslation?.First.String, "Get");
        Assert.Equal(addedTranslation?.First.Language, Language.English);
        Assert.Equal(addedTranslation?.Second.String, "Получить");
        Assert.Equal(addedTranslation?.Second.Language, Language.Russian);
    }

    [Fact]
    public async Task ShouldNotCreateTextIfAlreadyExists()
    {
        var command1 = new CreateTranslationCommand(
            new CreateTextRequest("One", Language.English),
            new CreateTextRequest("1", Language.Russian));

        var command2 = new CreateTranslationCommand(
            new CreateTextRequest("One", Language.English),
            new CreateTextRequest("Один", Language.Russian));

        var idDto1 = await _mediator.Send(command1);
        var idDto2 = await _mediator.Send(command2);

        var translation1 = await _context.Translations.FirstOrDefaultAsync(t => t.Id == idDto1.Id);
        var translation2 = await _context.Translations.FirstOrDefaultAsync(t => t.Id == idDto2.Id);

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

        var idDto1 = await _mediator.Send(command1);
        var idDto2 = await _mediator.Send(command2);

        Assert.Equal(idDto1, idDto2);
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
