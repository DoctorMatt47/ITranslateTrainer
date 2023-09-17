using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Tests.Domain.Unit.Builders;

namespace ITranslateTrainer.Tests.Domain.Unit.Entities;

public class OptionBuilder : HasIdBuilder<Option, int>
{
    private static readonly Faker Faker = new();

    public OptionBuilder()
    {
        Value = new Option
        {
            Text = new TextBuilder().Build(),
            IsCorrect = Faker.Random.Bool(),
        };

        WithId(Faker.Random.Int(0));
    }

    public OptionBuilder WithText(Text text)
    {
        SetPublicInstanceProperty(nameof(Value.Text), text);
        SetPublicInstanceProperty(nameof(Value.TextId), text.Id);
        return this;
    }
}
