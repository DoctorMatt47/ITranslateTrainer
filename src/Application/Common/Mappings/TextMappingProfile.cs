using AutoMapper;
using ITranslateTrainer.Application.Translations.Commands;
using ITranslateTrainer.Application.Translations.Queries;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Common.Mappings
{
    public class TextMappingProfile : Profile
    {
        public TextMappingProfile()
        {
            CreateMap<CreateTextRequest, Text>();
            CreateMap<Text, GetTextResponse>();
        }
    }
}
