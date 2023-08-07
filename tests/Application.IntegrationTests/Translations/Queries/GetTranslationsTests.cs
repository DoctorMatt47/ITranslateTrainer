using System.Collections.Generic;
using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Translations;
using ITranslateTrainer.Domain.Entities;
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
        var texts = new List<TranslationText>
        {
            new("One", "English"),
            new("Two", "English"),
            new("Один", "Russian"),
            new("Два", "Russian"),
        };

        var translations = new List<Translation>
        {
            new(texts[0], texts[2]),
            new(texts[1], texts[3]),
        };

        await _context.Set<TranslationText>().AddRangeAsync(texts);
        await _context.Set<Translation>().AddRangeAsync(translations);
        await _context.SaveChangesAsync();
    }
}
