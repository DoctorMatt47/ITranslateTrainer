using System.ComponentModel.DataAnnotations;
using ITranslateTrainer.Domain.Exceptions;

namespace ITranslateTrainer.Domain.ValueObjects;

public record TextString : WrapperBase<string>
{
    private static readonly StringLengthAttribute Attr = new(50)
    {
        MinimumLength = 2
    };

    private TextString(string value) : base(value.ToLowerInvariant())
    {
        if (!Attr.IsValid(value)) throw new DomainArgumentException("Invalid string length", nameof(value));
    }

    public static implicit operator TextString(string value) => new(value);
}