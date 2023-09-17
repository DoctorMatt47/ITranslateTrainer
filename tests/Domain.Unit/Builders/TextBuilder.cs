using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Tests.Domain.Unit.Builders;

public class TextBuilder : HasIdBuilder<Text, int>
{
    private static readonly Faker Faker = new();

    public TextBuilder()
    {
        Value = new Text
        {
            Value = Faker.Random.String(),
            Language = Faker.Random.String(),
        };

        WithId(Faker.Random.Int(0));
    }

    public TextBuilder WithTranslations(IEnumerable<Translation> translations)
    {
        SetPrivateInstanceField("_translations", translations.ToList());
        return this;
    }
}
