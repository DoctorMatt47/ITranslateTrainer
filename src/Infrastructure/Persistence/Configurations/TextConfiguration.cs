using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITranslateTrainer.Infrastructure.Persistence.Configurations;

public class TextConfiguration : IEntityTypeConfiguration<Text>
{
    public void Configure(EntityTypeBuilder<Text> builder)
    {
        builder.Property(t => t.String).HasConversion(v => v.Value, v => v).HasMaxLength(50).IsRequired();
    }
}
