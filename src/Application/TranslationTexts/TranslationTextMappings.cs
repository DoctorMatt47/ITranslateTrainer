using AutoMapper;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.TranslationTexts;

public class TranslationTextMappings : Profile
{
    public TranslationTextMappings()
    {
        CreateMap<Text, TextResponse>();
    }
}
