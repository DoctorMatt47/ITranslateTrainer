using ITranslateTrainer.Application.TranslationSheet.Responses;
using MediatR;
using MiniExcelLibs;

namespace ITranslateTrainer.Application.TranslationSheet.Requests;

public record ParseTranslationSheet(Stream SheetStream) : IRequest<IEnumerable<ParseTranslationResponse>>;

public record ParseTranslationSheetHandler :
    IRequestHandler<ParseTranslationSheet, IEnumerable<ParseTranslationResponse>>
{
    public async Task<IEnumerable<ParseTranslationResponse>> Handle(ParseTranslationSheet request,
        CancellationToken cancellationToken)
    {
        var sheet = await request.SheetStream.QueryAsync();
        var translations = sheet.Select(row =>
            new ParseTranslationResponse(row["A"]?.ToString(), row["B"]?.ToString(), row["C"]?.ToString(),
                row["D"]?.ToString())).ToList();
        request.SheetStream.Close();
        return translations;
    }
}