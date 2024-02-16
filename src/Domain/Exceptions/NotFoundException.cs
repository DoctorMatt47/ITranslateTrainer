using System.Runtime.CompilerServices;

namespace ITranslateTrainer.Domain.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string? message, Exception? innerException = null) : base(message, innerException) { }

    public static NotFoundException DoesNotExist<TEntity>(
        object propertyValue,
        [CallerArgumentExpression(nameof(propertyValue))] string propertyName = null!)
    {
        return new($"There is no {nameof(TEntity)} with {propertyName} '{propertyValue}'");
    }
}
