using AutoMapper;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.DayResults;

public class DayResultMappings : Profile
{
    public DayResultMappings()
    {
        CreateMap<DayResult, GetDayResultResponse>()
            .MapRecordMember(r => r.Day, d => d.Day.ToShortDateString());
    }
}
