using System.Reflection;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Infrastructure.Persistence;

public class TranslateDbContext : DbContext, ITranslateDbContext
{
    public TranslateDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Text> Texts { get; set; } = null!;
    public DbSet<Translation> Translations { get; set; } = null!;

    public Task SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
