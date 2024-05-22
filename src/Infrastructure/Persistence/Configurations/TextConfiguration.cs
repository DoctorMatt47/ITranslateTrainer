using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITranslateTrainer.Infrastructure.Persistence.Configurations;

public class TextConfiguration : IEntityTypeConfiguration<Text>
{
    public void Configure(EntityTypeBuilder<Text> builder)
    {
        builder.Property(t => t.Value).IsRequired();
        builder.Property(t => t.Language).IsRequired();

        builder.HasMany(t => t.OriginTextTranslations)
            .WithOne(t => t.OriginText)
            .HasForeignKey(t => t.OriginTextId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.TranslationTextTranslations)
            .WithOne(t => t.TranslationText)
            .HasForeignKey(t => t.TranslationTextId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
