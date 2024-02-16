using System.Linq;
using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.IntegrationTests.Application.Common;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace ITranslateTrainer.IntegrationTests.Application;

public class TextsControllerTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder().Build();
    private IAppDbContext _dbContext;
    private AsyncServiceScope _scope;
    private TestApplicationFactory Factory;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        var connectionString = _dbContainer.GetConnectionString();
        Factory = new(connectionString);
        _scope = Factory.Services.CreateAsyncScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<IAppDbContext>();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await Factory.DisposeAsync();
        await _scope.DisposeAsync();
    }

    [Fact]
    public async Task Patch_WithValidRequest_ReturnsNoContent()
    {
        // Arrange
        _dbContext.Set<Text>().Add(new()
        {
            Value = "Test",
            Language = "English",
        });

        await _dbContext.SaveChangesAsync();

        // Act
        // var response = await Client.PatchAsync("api/texts", );

        // Assert
        _dbContext.Set<Text>().First().Value.Should().Be("Test");
    }

    [Fact]
    public async Task Patch_WithValidRequest_ReturnsNoContent2()
    {
        // Arrange
        _dbContext.Set<Text>().Add(new()
        {
            Value = "Test",
            Language = "English",
        });

        await _dbContext.SaveChangesAsync();

        // Act
        // var response = await Client.PatchAsync("api/texts", );

        // Assert
        _dbContext.Set<Text>().First().Value.Should().Be("Test");
    }
}
