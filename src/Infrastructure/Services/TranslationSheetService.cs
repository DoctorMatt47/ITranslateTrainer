using ITranslateTrainer.Application.TranslationSheet;
using MiniExcelLibs;

namespace ITranslateTrainer.Infrastructure.Services;

public class TranslationSheetService : ITranslationSheetService
{
    public async Task<IEnumerable<ParseTranslationResponse>> ParseTranslations(Stream stream)
    {
        return (await stream.QueryAsync())
            .Where(row => row.A is not null
                && row.B is not null
                && row.C is not null
                && row.D is not null)
            .Select(row => new ParseTranslationResponse(
                new ParseTextResponse(row.C.ToString(), row.A.ToString()),
                new ParseTextResponse(row.D.ToString(), row.B.ToString())))
            .ToList();
    }
}
