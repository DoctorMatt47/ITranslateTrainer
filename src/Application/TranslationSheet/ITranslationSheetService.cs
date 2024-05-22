namespace ITranslateTrainer.Application.TranslationSheet;

public interface ITranslationSheetService
{
    Task<IEnumerable<ParseTranslationResponse>> ParseTranslations(Stream stream);
}
