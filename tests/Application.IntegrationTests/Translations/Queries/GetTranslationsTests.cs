using System.Collections.Generic;
using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Translations.Queries;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;
using MediatR;
using Xunit;

namespace ITranslateTrainer.Application.IntegrationTests.Translations.Queries;

public class GetTranslationsTests
{
    private readonly ITranslateDbContext _context;
    private readonly IMediator _mediator;

    public GetTranslationsTests(IMediator mediator, ITranslateDbContext context)
    {
        _mediator = mediator;
        _context = context;

        InitDb().GetAwaiter().GetResult();
    }

    [Fact]
    public async Task ShouldReturnAllTranslations()
    {
        var translations = await _mediator.Send(new GetTranslationsQuery());

        Assert.NotNull(translations);
        Assert.NotEmpty(translations);
    }

    private async Task InitDb()
    {
        var texts = new List<Text>
        {
            new(TextString.From("One"), Language.English),
            new(TextString.From("Two"), Language.English),
            new(TextString.From("Один"), Language.Russian),
            new(TextString.From("Два"), Language.Russian)
        };

        var translations = new List<Translation>
        {
            new(texts[0], texts[2]),
            new(texts[1], texts[3])
        };

        await _context.Set<Text>().AddRangeAsync(texts);
        await _context.Set<Translation>().AddRangeAsync(translations);
        await _context.SaveChangesAsync();
    }
}
