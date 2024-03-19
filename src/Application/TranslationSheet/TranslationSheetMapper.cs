using ITranslateTrainer.Application.Texts;
using Riok.Mapperly.Abstractions;

namespace ITranslateTrainer.Application.TranslationSheet;

[Mapper]
public static partial class TranslationSheetMapper
{
    public static partial TextRequest MapToRequest(this ParseTextResponse translationSheet);
}
