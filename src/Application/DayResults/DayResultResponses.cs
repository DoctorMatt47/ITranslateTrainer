namespace ITranslateTrainer.Application.DayResults;

public record GetDayResultResponse(DateOnly Day, int CorrectCount, int IncorrectCount);
