using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.TranslationSheet.Responses;
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
                row.A.ToString()!,
                row.B.ToString()!,
                row.C.ToString()!,
                row.D.ToString()!))
            .ToList();
    }
}