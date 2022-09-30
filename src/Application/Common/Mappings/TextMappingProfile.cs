using AutoMapper;
using ITranslateTrainer.Application.Texts.Responses;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Common.Mappings;

public class TextMappingProfile : Profile
{
    public TextMappingProfile()
    {
        CreateMap<Text, GetTextResponse>()
            .ForMember(t => t.Language, c => c.MapFrom(t => Enum.GetName(t.Language)));
    }
}
