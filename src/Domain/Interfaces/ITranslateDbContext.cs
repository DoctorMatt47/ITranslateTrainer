using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Domain.Interfaces;

public interface ITranslateDbContext
{
    public DbSet<T> Set<T>() where T : class;

    public Task SaveChangesAsync();
}