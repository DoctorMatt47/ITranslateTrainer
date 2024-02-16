using System;
using System.Net.Http;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace ITranslateTrainer.IntegrationTests.Application.Common;

public abstract class TestBase : IAsyncDisposable
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder().Build();

    protected readonly HttpClient Client;
    protected readonly TestApplicationFactory Factory;

    protected TestBase()
    {
        _dbContainer.StartAsync().GetAwaiter().GetResult();
        Factory = new(_dbContainer.GetConnectionString());
        Client = Factory.CreateClient();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        Client.Dispose();
        await Factory.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
