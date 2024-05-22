using ITranslateTrainer.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ITranslateTrainer.Application.Translations;

[Mapper]
public static partial class TranslationMapper
{
    public static partial IQueryable<TranslationResponse> ProjectToResponse(this IQueryable<Translation> translation);
    public static partial TranslationResponse ToResponse(this Translation translation);
}
