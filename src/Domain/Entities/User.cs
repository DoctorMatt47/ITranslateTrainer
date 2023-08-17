using ITranslateTrainer.Domain.Abstractions;

namespace ITranslateTrainer.Domain.Entities;

public class User : HasId<int>
{
    private readonly List<Translation> _translations = new();

    public required string Login { get; init; } = null!;
    public required byte[] PasswordSalt { get; init; } = null!;
    public required byte[] PasswordHash { get; init; } = null!;

    public IEnumerable<Translation> Translations => _translations.AsReadOnly();

    public void AddTranslation(Translation translation)
    {
        _translations.Add(translation);
    }
}
