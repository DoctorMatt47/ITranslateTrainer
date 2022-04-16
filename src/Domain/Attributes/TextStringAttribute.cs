using System.ComponentModel.DataAnnotations;

namespace ITranslateTrainer.Domain.Attributes;

public class TextStringAttribute : ValidationAttribute
{
    private static readonly StringLengthAttribute Attr = new(50)
    {
        MinimumLength = 2
    };

    public override bool IsValid(object? value) => value is string && Attr.IsValid(value);
}