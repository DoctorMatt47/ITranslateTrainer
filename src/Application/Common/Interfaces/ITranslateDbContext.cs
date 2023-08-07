using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Common.Interfaces;

public interface ITranslateDbContext
{
    public DbSet<T> Set<T>() where T : class;
    public Task SaveChangesAsync();
}
