using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITranslateTrainer.Infrastructure.Persistence.Configurations;

public class DayResultConfiguration : IEntityTypeConfiguration<DayResult>
{
    public void Configure(EntityTypeBuilder<DayResult> builder)
    {
        builder.HasKey(d => d.Day);
    }
}
