using System.Runtime.CompilerServices;

namespace ITranslateTrainer.Domain.Exceptions;

public class ConflictException : AppException
{
    public ConflictException(string? message, Exception? innerException = null) : base(message, innerException) { }

    public static ConflictException AlreadyExists<TEntity>(
        object propertyValue,
        [CallerArgumentExpression(nameof(propertyValue))] string propertyName = null!)
    {
        return new($"There is already a {nameof(TEntity)} with {propertyName} '{propertyValue}'");
    }
}
