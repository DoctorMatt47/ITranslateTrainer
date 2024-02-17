using System;
using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace ITranslateTrainer.IntegrationTests.Application.Common;

public class TestApplicationFactory : WebApplicationFactory<Program>, IAsyncDisposable
{
    private readonly string _connectionString;
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder().Build();

    public TestApplicationFactory()
    {
        _dbContainer.StartAsync().GetAwaiter().GetResult();
        _connectionString = _dbContainer.GetConnectionString();
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<DbContextOptions<AppDbContext>>()
                .AddDbContext<IAppDbContext, AppDbContext>(opts => { opts.UseNpgsql(_connectionString); });
        });
    }
}
