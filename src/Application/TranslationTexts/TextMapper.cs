using ITranslateTrainer.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ITranslateTrainer.Application.TranslationTexts;

[Mapper]
public static partial class TextMapper
{
    public static partial TextResponse ToResponse(this Text text);
}
