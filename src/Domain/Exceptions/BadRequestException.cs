namespace ITranslateTrainer.Domain.Exceptions;

public class BadRequestException(string? message, Exception? innerException = null)
    : AppException(message, innerException);
