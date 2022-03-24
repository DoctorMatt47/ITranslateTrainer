using AutoMapper;
using ITranslateTrainer.Application.Translations.Responses;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Common.Mappings;

public class TranslationMappingProfile : Profile
{
    public TranslationMappingProfile()
    {
        CreateMap<Translation, GetTranslationResponse>();
    }
}