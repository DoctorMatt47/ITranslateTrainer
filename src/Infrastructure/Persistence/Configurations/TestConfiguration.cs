﻿using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITranslateTrainer.Infrastructure.Persistence.Configurations;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasMany(t => t.Options).WithOne(o => o.Test);
        builder.HasOne(t => t.Text);
    }
}
