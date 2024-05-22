using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.IntegrationTests.Application.Common;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace ITranslateTrainer.IntegrationTests.Application;

public class TextsControllerTests(TestApplicationFixture app) : IClassFixture<TestApplicationFixture>
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder().Build();
    private TestApplicationFactory _factory = null!;
    private AsyncServiceScope _scope;

    internal async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _factory = new TestApplicationFactory();
        _scope = _factory.Services.CreateAsyncScope();
    }

    internal async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await _factory.DisposeAsync();
        await _scope.DisposeAsync();
    }

    [Fact]
    public async Task Patch_WithNoTextId_ReturnsNotFound()
    {
        // Arrange
        var textId = new Faker().Random.Int(min: 0);

        // Act
        var request = new {Id = 0, Text = new Faker().Random.Utf16String()};
        var response = await app.Client.PatchAsync($"api/texts/{textId}", JsonContent.Create(request));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Patch_WithValidRequest_UpdatesTextValueInDb()
    {
        // Arrange
        var text = new Faker<Text>()
            .RuleFor(t => t.Value, f => f.Random.Utf16String())
            .RuleFor(t => t.Language, f => f.Random.Utf16String())
            .Generate();

        await app.CreateEntity(text);

        // Act
        var request = new {Text = new Faker().Random.Utf16String()};
        var response = await app.Client.PatchAsync($"api/texts/{text.Id}", JsonContent.Create(request));

        // Assert
        var patchedText = await app.FindEntity<Text>(text.Id);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        patchedText!.Value.Should().Be(request.Text);
    }
}
