using System.Runtime.CompilerServices;

namespace ITranslateTrainer.Domain.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string? message, Exception? innerException = null) : base(message, innerException)
    {
    }

    public static NotFoundException DoesNotExist(
        string entityName,
        object propertyValue,
        [CallerArgumentExpression(nameof(propertyValue))] string propertyName = null!) =>
        new($"There is no {entityName} with {propertyName} '{propertyValue}'");
}
