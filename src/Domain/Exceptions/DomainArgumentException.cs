namespace ITranslateTrainer.Domain.Exceptions;

public class DomainArgumentException : ArgumentException
{
    public DomainArgumentException(string? message, string? paramName) : base(message, paramName)
    {
    }

    public DomainArgumentException(string? message, string? paramName, Exception inner) :
        base(message, paramName, inner)
    {
    }
}