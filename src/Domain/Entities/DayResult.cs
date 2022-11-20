using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.Entities;

public class DayResult : IHasId<DateOnly>
{
    public DayResult(DateOnly day) => Day = day;

    public DateOnly Day { get; protected set; }
    public int CorrectCount { get; protected set; }
    public int IncorrectCount { get; protected set; }

    public DateOnly Id => Day;

    public void GotAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            CorrectCount++;
            return;
        }

        CorrectCount--;
    }
}
