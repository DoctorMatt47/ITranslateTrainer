using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Tests.Domain.Unit.Builders;

public class UserBuilder : HasIdBuilder<User, int>
{
    private static readonly Faker Faker = new();

    public UserBuilder()
    {
        Value = new User
        {
            Login = Faker.Random.String(),
            PasswordSalt = Faker.Random.Bytes(32),
            PasswordHash = Faker.Random.Bytes(32),
        };

        WithId(Faker.Random.Int(0));
    }
}
