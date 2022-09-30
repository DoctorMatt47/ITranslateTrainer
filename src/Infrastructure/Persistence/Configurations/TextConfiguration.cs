using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using ITranslateTrainer.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITranslateTrainer.Infrastructure.Persistence.Configurations;

public class TextConfiguration : IEntityTypeConfiguration<Text>
{
    public void Configure(EntityTypeBuilder<Text> builder)
    {
        builder.Property(t => t.String)
            .HasConversion(v => v.Value.ToLowerInvariant(), v => TextString.From(v))
            .IsRequired();

        builder.Property(t => t.Language)
            .HasConversion(v => Enum.GetName(v), v => Enum.Parse<Language>(v!));
    }
}
