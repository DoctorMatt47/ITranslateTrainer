using AutoMapper;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;
using Mapster;

namespace ITranslateTrainer.Application.Tests;

public class TestMappings : Profile
{
    public TestMappings()
    {
        CreateMap<Test, GetOrCreateTestResponse>()
            .MapRecordMember(r => r.String, o => o.TranslationText.Text);

        CreateMap<Option, GetOrCreateOptionResponse>()
            .MapRecordMember(r => r.String, o => o.TranslationText.Text);

        CreateMap<Test, TestResponse>()
            .MapRecordMember(r => r.String, t => t.TranslationText.Text)
            .MapRecordMember(r => r.AnswerTime, t => t.AnswerTime!.Value.ToString("MM/dd/yyyy h:mm tt"));

        CreateMap<Option, OptionResponse>()
            .MapRecordMember(r => r.String, t => t.TranslationText.Text);
    }
}

[Mapper]
public interface ITestMapper
{
    public GetOrCreateTestResponse MapToGetOrCreateTestResponse(Test test);
    public GetOrCreateOptionResponse MapToGetOrCreateOptionResponse(Option option);
    public TestResponse MapToTestResponse(Test test);
    public OptionResponse MapToOptionResponse(Option option);
}
