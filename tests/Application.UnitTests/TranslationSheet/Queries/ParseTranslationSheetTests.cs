using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ITranslateTrainer.Application.TranslationSheet.Requests;
using MediatR;
using Xunit;

namespace ITranslateTrainer.Application.UnitTests.TranslationSheet.Queries;

public class ParseTranslationSheetTests
{
    private readonly IMediator _mediator;

    public ParseTranslationSheetTests(IMediator mediator) => _mediator = mediator;

    [Fact]
    public async Task ShouldParseExcelFile()
    {
        var fileStream = File.OpenRead("Assets/TestSheet.xlsx");
        var translations = (await _mediator.Send(new ParseTranslationSheet(fileStream))).ToList();
        Assert.NotNull(translations);
        Assert.Equal(233, translations.Count);
        foreach (var translation in translations)
        {
            Assert.NotNull(translation.FirstLanguage);
            Assert.NotNull(translation.SecondLanguage);
            Assert.NotNull(translation.FirstText);
            Assert.NotNull(translation.SecondText);
        }
    }
}