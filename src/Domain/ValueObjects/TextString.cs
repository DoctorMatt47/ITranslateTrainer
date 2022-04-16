using ITranslateTrainer.Domain.Attributes;
using ITranslateTrainer.Domain.Exceptions;

namespace ITranslateTrainer.Domain.ValueObjects;

public record TextString : WrapperBase<string>
{
    private static readonly TextStringAttribute Attr = new();

    private TextString(string value) : base(Filtered(value))
    {
        if (!Attr.IsValid(value)) throw new DomainArgumentException("Invalid string length", nameof(value));
    }

    private static string Filtered(string value) => value.ToLowerInvariant().Replace("\n", "").Replace("\t", "").Trim();

    public static implicit operator TextString(string value) => new(value);
}
