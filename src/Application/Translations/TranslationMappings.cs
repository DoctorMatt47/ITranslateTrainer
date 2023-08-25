using AutoMapper;
using ITranslateTrainer.Application.TranslationTexts;
using ITranslateTrainer.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ITranslateTrainer.Application.Translations;

public class TranslationMappings : Profile
{
    public TranslationMappings()
    {
        CreateMap<Translation, TranslationResponse>();
    }
}

[Mapper]
public partial class TranslationMapper
{
    public partial TextResponse ToResponse(Text text);
}
