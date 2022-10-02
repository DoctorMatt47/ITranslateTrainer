using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Translations.Commands;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ITranslateTrainer.Application.IntegrationTests.Translations.Commands;

public class DeleteTranslationTests
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public DeleteTranslationTests(IMediator mediator, ITranslateDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [Fact]
    public async Task ShouldDeleteTranslation()
    {
        var translationToAdd = new Translation(
            new Text("delete translation", "English"),
            new Text("удаление перевода", "Russian"));

        await _context.Set<Translation>().AddAsync(translationToAdd);
        await _context.SaveChangesAsync();

        await _mediator.Send(new DeleteTranslationCommand(translationToAdd.Id));

        var translation = await _context.Set<Translation>().FirstOrDefaultAsync(t => t.Id == translationToAdd.Id);
        Assert.Null(translation);

        var first = await _context.Set<Text>().FirstOrDefaultAsync(t => t.Id == translationToAdd.FirstId);
        Assert.Null(first);

        var second = await _context.Set<Text>().FirstOrDefaultAsync(t => t.Id == translationToAdd.SecondId);
        Assert.Null(second);
    }
}