using ITranslateTrainer.Domain.Attributes;
using ITranslateTrainer.Domain.Exceptions;
using ValueOf;

namespace ITranslateTrainer.Domain.ValueObjects;

public class TextString : ValueOf<string, TextString>
{
    private static readonly TextStringAttribute Attr = new();

    protected override void Validate()
    {
        if (!Attr.IsValid(Value)) throw new DomainArgumentException("Invalid string length", nameof(Value));
    }
}
