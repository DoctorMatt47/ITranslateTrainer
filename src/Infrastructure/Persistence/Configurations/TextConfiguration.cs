using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITranslateTrainer.Infrastructure.Persistence.Configurations;

public class TextConfiguration : IEntityTypeConfiguration<TranslationText>
{
    public void Configure(EntityTypeBuilder<TranslationText> builder)
    {
        builder.Property(t => t.String).IsRequired();
        builder.Property(t => t.Language).IsRequired();
    }
}
