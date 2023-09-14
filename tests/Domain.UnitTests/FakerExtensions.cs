using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Domain.UnitTests;

public static class FakerExtensions
{
    public static User User(this Faker faker) => new()
    {
        Login = faker.Random.String(),
        PasswordSalt = faker.Random.Bytes(32),
        PasswordHash = faker.Random.Bytes(32),
    };

    public static Text Text(this Faker faker) => new()
    {
        Value = faker.Random.String(),
        Language = faker.Random.String(),
    };

    public static Translation Translation(this Faker faker) => new()
    {
        CanBeOption = faker.Random.Bool(),
        OriginText = faker.Text(),
        TranslationText = faker.Text(),
    };
}
