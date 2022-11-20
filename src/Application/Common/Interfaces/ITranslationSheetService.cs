using ITranslateTrainer.Application.TranslationSheet;

namespace ITranslateTrainer.Application.Common.Interfaces;

public interface ITranslationSheetService
{
    Task<IEnumerable<ParseTranslationResponse>> ParseTranslations(Stream stream);
}
