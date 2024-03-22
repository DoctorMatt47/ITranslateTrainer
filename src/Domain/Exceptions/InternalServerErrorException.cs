namespace ITranslateTrainer.Domain.Exceptions;

public class InternalServerErrorException(string message) : AppException(message);
