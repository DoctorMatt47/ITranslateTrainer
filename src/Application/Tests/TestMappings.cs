using AutoMapper;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Tests;

public class TestMappings : Profile
{
    public TestMappings()
    {
        CreateMap<Test, GetOrCreateTestResponse>()
            .MapRecordMember(r => r.String, o => o.TranslationText.String);

        CreateMap<Option, GetOrCreateOptionResponse>()
            .MapRecordMember(r => r.String, o => o.TranslationText.String);

        CreateMap<Test, TestResponse>()
            .MapRecordMember(r => r.String, t => t.TranslationText.String)
            .MapRecordMember(r => r.AnswerTime, t => t.AnswerTime!.Value.ToString("MM/dd/yyyy h:mm tt"));

        CreateMap<Option, OptionResponse>()
            .MapRecordMember(r => r.String, t => t.TranslationText.String);
    }
}
