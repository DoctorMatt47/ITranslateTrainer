using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Tests.Domain.Unit.Entities;

namespace ITranslateTrainer.Tests.Domain.Unit.Builders;

public class TestBuilder : HasIdBuilder<Test, int>
{
    private static readonly Faker Faker = new();

    public TestBuilder()
    {
        Value = new Test
        {
            Text = new TextBuilder().Build(),
            Options = Faker.Make(Faker.Random.Int(2, 100), () => new OptionBuilder().Build()).DistinctBy(o => o.Id),
        };

        WithId(Faker.Random.Int(0));
    }
}
