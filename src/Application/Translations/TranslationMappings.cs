using AutoMapper;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Translations;

public class TranslationMappings : Profile
{
    public TranslationMappings()
    {
        CreateMap<Translation, GetTranslationResponse>();
    }
}
