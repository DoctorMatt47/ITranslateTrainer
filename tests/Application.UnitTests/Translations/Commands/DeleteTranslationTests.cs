using System.Threading.Tasks;
using ITranslateTrainer.Application.Translations.Commands;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ITranslateTrainer.Application.UnitTests.Translations.Commands;

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
        var translationToAdd = new Translation
        {
            First = new Text
            {
                Language = Language.English,
                String = "delete translation"
            },
            Second = new Text
            {
                Language = Language.Russian,
                String = "удаление перевода"
            }
        };
        await _context.Translations.AddAsync(translationToAdd);
        await _context.SaveChangesAsync();

        await _mediator.Send(new DeleteTranslationCommand(translationToAdd.Id));
        var translation = await _context.Translations.FirstOrDefaultAsync(t => t.Id == translationToAdd.Id);
        Assert.Null(translation);

        var first = await _context.Texts.FirstOrDefaultAsync(t => t.Id == translationToAdd.FirstId);
        Assert.Null(first);

        var second = await _context.Texts.FirstOrDefaultAsync(t => t.Id == translationToAdd.SecondId);
        Assert.Null(second);
    }
}
