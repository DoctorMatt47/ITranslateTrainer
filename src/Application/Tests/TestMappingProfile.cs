using AutoMapper;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Tests.Responses;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Tests;

public class TestMappingProfile : Profile
{
    public TestMappingProfile()
    {
        CreateMap<Test, GetOrCreateTestResponse>()
            .MapRecordMember(r => r.String, o => o.Text.String);

        CreateMap<Option, GetOrCreateOptionResponse>()
            .MapRecordMember(r => r.String, o => o.Text.String);

        CreateMap<Test, GetTestResponse>()
            .MapRecordMember(r => r.String, t => t.Text.String);

        CreateMap<Option, GetOptionResponse>()
            .MapRecordMember(r => r.String, t => t.Text.String);
    }
}
