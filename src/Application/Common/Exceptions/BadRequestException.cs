namespace ITranslateTrainer.Application.Common.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string? msg) : base(msg)
    {
    }

    public BadRequestException(string? msg, Exception? inner) : base(msg, inner)
    {
    }
}