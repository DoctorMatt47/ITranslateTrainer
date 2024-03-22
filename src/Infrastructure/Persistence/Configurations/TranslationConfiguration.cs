﻿using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITranslateTrainer.Infrastructure.Persistence.Configurations;

public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.HasOne(t => t.OriginText).WithMany(t => t.Translations);
        builder.HasOne(t => t.TranslationText);

        builder.Navigation(t => t.OriginText).AutoInclude();
        builder.Navigation(t => t.TranslationText).AutoInclude();
    }
}
