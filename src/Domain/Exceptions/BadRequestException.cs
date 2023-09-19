namespace ITranslateTrainer.Domain.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException(string? message, Exception? innerException = null) : base(message, innerException)
    {
    }
}
