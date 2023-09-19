namespace ITranslateTrainer.Domain.Exceptions;

public class ConflictException : AppException
{
    public ConflictException(string? message, Exception? innerException = null) : base(message, innerException)
    {
    }
}
