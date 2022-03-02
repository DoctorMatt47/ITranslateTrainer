using MediatR;
using MiniExcelLibs;

namespace ITranslateTrainer.Application.TranslationSheet.Queries;

public record ParseTranslationSheetQuery(Stream SheetStream) : IRequest<IEnumerable<ParseTranslationResponse>>;

public record ParseTranslationSheetQueryHandler :
    IRequestHandler<ParseTranslationSheetQuery, IEnumerable<ParseTranslationResponse>>
{
    public async Task<IEnumerable<ParseTranslationResponse>> Handle(ParseTranslationSheetQuery request,
        CancellationToken cancellationToken)
    {
        var sheet = await request.SheetStream.QueryAsync();
        return sheet.Select(row => new ParseTranslationResponse(row["A"].ToString(), row["B"].ToString(),
            row["C"].ToString(), row["D"].ToString())).ToList();
    }
}
