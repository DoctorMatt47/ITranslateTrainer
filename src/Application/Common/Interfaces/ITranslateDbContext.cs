using ITranslateTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Common.Interfaces;

public interface ITranslateDbContext
{
    public DbSet<Text> Texts { get; set; }
    public DbSet<Translation> Translations { get; set; }

    public Task SaveChangesAsync();
}