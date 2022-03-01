using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Domain.Interfaces;

public interface ITranslateDbContext
{
    public DbSet<Text> Texts { get; set; }
    public DbSet<Translation> Translations { get; set; }

    public Task SaveChangesAsync();
}