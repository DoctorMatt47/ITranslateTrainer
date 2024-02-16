namespace ITranslateTrainer.Domain.Exceptions;

public abstract class AppException : Exception
{
    protected AppException(string? message, Exception? innerException = null) : base(message, innerException) { }
}
