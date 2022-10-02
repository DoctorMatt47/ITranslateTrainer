using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ITranslateTrainer.Infrastructure.Services;
using Xunit;

namespace ITranslateTrainer.Infrastructure.UnitTests.Services;

public class TranslationSheetServiceTests
{
    private static readonly TranslationSheetService TranslationSheetService = new();

    [Fact]
    public async Task ShouldReturn233TranslationsFromSheet()
    {
        var sheetStream = File.OpenRead("Assets/TestTranslationSheet.xlsx");
        var response = await TranslationSheetService.ParseTranslations(sheetStream);
        Assert.Equal(233, response.Count());
    }
}