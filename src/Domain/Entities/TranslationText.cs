﻿using ITranslateTrainer.Domain.Interfaces;

namespace ITranslateTrainer.Domain.Entities;

public class TranslationText : IHasId<int>
{
    protected TranslationText()
    {
    }

    public TranslationText(string @string, string language)
    {
        String = @string.ToLowerInvariant();
        Language = language.ToLowerInvariant();
    }

    public string String { get; protected set; } = null!;
    public string Language { get; protected set; } = null!;

    public bool CanBeOption { get; set; } = true;
    public bool CanBeTested { get; set; } = true;

    public int CorrectCount { get; set; }
    public int IncorrectCount { get; set; }

    public int Id { get; protected set; }

    public void Answer(bool isCorrect)
    {
        if (isCorrect)
        {
            CorrectCount++;
            return;
        }

        IncorrectCount++;
    }
}
