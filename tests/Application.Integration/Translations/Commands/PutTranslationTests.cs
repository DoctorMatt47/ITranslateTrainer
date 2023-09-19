using System.Threading.Tasks;
using Bogus;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Translations;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ITranslateTrainer.Tests.Application.Integration.Translations.Commands;

public class PutTranslationTests
{
    private readonly ITranslateDbContext _context;
    private readonly Faker _faker = new();
    private readonly IMediator _mediator;

    public PutTranslationTests(IMediator mediator, ITranslateDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [Fact]
    public async Task ShouldCreateTranslation()
    {
        var command = new Faker<PutTranslationCommand>().Generate();

        var translationResponse = await _mediator.Send(command);

        var addedTranslation =
            await _context.Set<Translation>().FirstOrDefaultAsync(t => t.Id == translationResponse.Id);

        Assert.NotNull(addedTranslation);
        Assert.Equal(command.FirstText, addedTranslation!.OriginText.Value);
        Assert.Equal(command.FirstLanguage, addedTranslation.OriginText.Language);
        Assert.Equal(command.SecondText, addedTranslation.TranslationText.Value);
        Assert.Equal(command.SecondLanguage, addedTranslation.TranslationText.Language);
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

        Assert.Same(translation1?.OriginText, translation2?.OriginText);
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
