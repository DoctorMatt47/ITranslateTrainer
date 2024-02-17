using System.Net.Http;
using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.IntegrationTests.Application.Common;

public class TestApplicationFixture : IAsyncLifetime
{
    public TestApplicationFactory Factory { get; private set; } = null!;
    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Factory = new TestApplicationFactory();
        Client = Factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await Factory.DisposeAsync();
        Client.Dispose();
    }

    public async Task CreateEntity<TEntity>(TEntity entity) where TEntity : class
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

        dbContext.Set<TEntity>().Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> FindEntity<TEntity>(object id) where TEntity : class
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

        return await dbContext.Set<TEntity>().FindAsync(id);
    }
}
