using System.Collections.Generic;
using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Translations.Queries;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using MediatR;
using Xunit;

namespace ITranslateTrainer.Application.UnitTests.Translations.Queries;

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
            new() {Language = Language.English, String = "One"},
            new() {Language = Language.English, String = "Two"},
            new() {Language = Language.Russian, String = "Один"},
            new() {Language = Language.Russian, String = "Два"}
        };
        var translations = new List<Translation>
        {
            new() {First = texts[0], Second = texts[2]},
            new() {First = texts[1], Second = texts[3]}
        };
        await _context.Texts.AddRangeAsync(texts);
        await _context.Translations.AddRangeAsync(translations);
        await _context.SaveChangesAsync();
    }
}