using System.Runtime.CompilerServices;

namespace ITranslateTrainer.Domain.Exceptions;

public class ConflictException(string? message, Exception? innerException = null)
    : AppException(message, innerException)
{
    public static ConflictException AlreadyExists<TEntity>(
        object propertyValue,
        [CallerArgumentExpression(nameof(propertyValue))] string propertyName = null!)
    {
        return new ConflictException($"There is already a {nameof(TEntity)} with {propertyName} '{propertyValue}'");
    }
}
