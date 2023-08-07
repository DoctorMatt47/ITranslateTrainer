using AutoMapper;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Texts;

public class TextMappings : Profile
{
    public TextMappings()
    {
        CreateMap<TranslationText, TranslationTextResponse>();
    }
}
