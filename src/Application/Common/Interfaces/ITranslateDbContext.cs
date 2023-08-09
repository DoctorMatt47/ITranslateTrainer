using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Common.Interfaces;

public interface ITranslateDbContext
{
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
