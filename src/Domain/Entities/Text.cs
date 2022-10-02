using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.Entities;

public class Text : IHasId<int>
{
    protected Text()
    {
    }

    public Text(string @string, string language)
    {
        String = @string.ToLowerInvariant();
        Language = language.ToLowerInvariant();
    }

    public string String { get; protected set; } = null!;
    public string Language { get; protected set; } = null!;

    public bool CanBeOption { get; set; } = true;
    public bool CanBeTested { get; set; } = true;

    public int Id { get; protected set; }
}
