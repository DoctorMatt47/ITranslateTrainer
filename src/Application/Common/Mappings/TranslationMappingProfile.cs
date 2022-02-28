using AutoMapper;
using ITranslateTrainer.Application.Translations.Queries;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Common.Mappings
{
    public class TranslationMappingProfile : Profile
    {
        public TranslationMappingProfile()
        {
            CreateMap<Translation, GetTranslationResponse>();
        }
    }
}
