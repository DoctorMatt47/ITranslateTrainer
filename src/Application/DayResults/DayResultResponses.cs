namespace ITranslateTrainer.Application.DayResults;

public record GetDayResultResponse(string Day, int CorrectCount, int IncorrectCount);
