using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITranslateTrainer.Infrastructure.Persistence.Configurations;

public class TextConfiguration : IEntityTypeConfiguration<Text>
{
    public void Configure(EntityTypeBuilder<Text> builder)
    {
        builder.Property(t => t.String)
            .HasConversion(v => LoweredAndFiltered(v.Value), v => TextString.From(v))
            .IsRequired();
    }

    private static string LoweredAndFiltered(string value) =>
        value.ToLowerInvariant().Replace("\n", "").Replace("\t", "").Trim();
}
