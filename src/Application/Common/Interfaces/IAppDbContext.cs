using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
