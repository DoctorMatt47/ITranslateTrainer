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
    private AsyncServiceScope _scope;
    private TestApplicationFactory Factory = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        Factory = new TestApplicationFactory();
        _scope = Factory.Services.CreateAsyncScope();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await Factory.DisposeAsync();
        await _scope.DisposeAsync();
    }

    [Fact]
    public async Task Patch_WithNoTextId_ReturnsNotFound()
    {
        // Arrange
        var id = new Faker().Random.Int();

        // Act
        var request = new {Text = new Faker().Random.Utf16String()};
        var response = await app.Client.PatchAsync($"api/texts/{id}", JsonContent.Create(request));

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
