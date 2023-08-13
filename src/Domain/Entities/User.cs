namespace ITranslateTrainer.Domain.Entities;

public class User
{
    public string Login { get; private set; } = null!;
    public required byte[] PasswordSalt { get; init; } = null!;
    public required byte[] PasswordHash { get; init; } = null!;
}
