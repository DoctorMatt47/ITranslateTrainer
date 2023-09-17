using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Tests.Domain.Unit.Builders;

public class TranslationBuilder : HasIdBuilder<Translation, int>
{
    private static readonly Faker Faker = new();

    public TranslationBuilder()
    {
        Value = new Translation
        {
            CanBeOption = Faker.Random.Bool(),
            OriginText = new TextBuilder().Build(),
            TranslationText = new TextBuilder().Build(),
        };

        WithId(Faker.Random.Int(0));
    }

    public TranslationBuilder WithOriginText(Text text)
    {
        SetPublicInstanceProperty(nameof(Value.OriginText), text);
        SetPublicInstanceProperty(nameof(Value.OriginTextId), text.Id);
        return this;
    }

    public TranslationBuilder WithTranslationText(Text text)
    {
        SetPublicInstanceProperty(nameof(Value.TranslationText), text);
        SetPublicInstanceProperty(nameof(Value.TranslationTextId), text.Id);
        return this;
    }

    public TranslationBuilder WithText(Text text) => Faker.PickRandom(WithTranslationText, WithOriginText).Invoke(text);
}
