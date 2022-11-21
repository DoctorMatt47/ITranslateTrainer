using AutoMapper;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.DayResults;

public class DayResultMappings : Profile
{
    public DayResultMappings()
    {
        CreateMap<DayResult, GetDayResultResponse>();
    }
}
