using AutoMapper;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;
using Mapper = Riok.Mapperly.Abstractions.MapperAttribute;

namespace ITranslateTrainer.Application.Tests;

public class TestMappings : Profile
{
    public TestMappings()
    {
        CreateMap<Test, GetOrCreateTestResponse>()
            .MapRecordMember(r => r.String, o => o.Text.Value);

        CreateMap<Option, GetOrCreateOptionResponse>()
            .MapRecordMember(r => r.String, o => o.Text.Value);

        CreateMap<Test, TestResponse>()
            .MapRecordMember(r => r.String, t => t.Text.Value)
            .MapRecordMember(r => r.AnswerTime, t => t.AnswerTime!.Value.ToString("MM/dd/yyyy h:mm tt"));

        CreateMap<Option, OptionResponse>()
            .MapRecordMember(r => r.TranslationText, t => t.Text.Value);
    }
}
