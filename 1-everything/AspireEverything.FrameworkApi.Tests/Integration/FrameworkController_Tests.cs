namespace AspireEverything.FrameworkApi.Tests.Integration;

[Collection("Sequential")]
public class FrameworkController_Tests(ITestOutputHelper output) : IAsyncLifetime
{

    public async Task InitializeAsync() =>
        await DatabaseFixture.ResetDatabase();

    public async Task DisposeAsync() =>
        await DatabaseFixture.ResetDatabase();

    [Fact]
    [Trait("Type", "Integration")]
    [Trait("CreateData", "true")]
    public async Task GetFrameworks_ReturnsSuccess()
    {
        // Arrange
        using var app = new FrameworkApiApp(output);
        var client = app.CreateClient();

        // Act
        var response = await client.GetAsync("/api/framework");
        response.EnsureSuccessStatusCode();
        var frameworks = await response.Content.ReadFromJsonAsync<List<Framework>>();

        // Assert
        frameworks.ShouldNotBeNull();
        frameworks.ShouldBeEmpty(); // because we reset the database before each test
    }

    // Yes, it's a long test, but it ensures the full CRUD cycle works
    [Fact]
    [Trait("Type", "Integration")]
    [Trait("CreateData", "true")]
    public async Task Create_Get_Update_Get_Delete()
    {
        // Arrange
        using var app = new FrameworkApiApp(output);
        var client = app.CreateClient();
        var newFramework = new Framework
        {
            Name = "Test Framework"
        };

        // Act & Assert
        await Create(newFramework, client);
        await GetById(newFramework, client);
        await Update(newFramework, client);
        await GetById(newFramework, client);
        await Delete(newFramework, client);
        await GetByIdNotFound(newFramework, client);
    }

    private async Task Create(Framework newFramework, HttpClient client)
    {

        // Act
        var response = await client.PostAsJsonAsync("/api/framework", newFramework);
        response.EnsureSuccessStatusCode();
        var createdFramework = await response.Content.ReadFromJsonAsync<Framework>();

        // Assert
        createdFramework.ShouldNotBeNull();
        ArgumentNullException.ThrowIfNull(createdFramework);
        createdFramework.Id.ShouldBeGreaterThan(0);
        createdFramework.Name.ShouldBe(newFramework.Name);
    }

    private async Task GetById(Framework expectedFramework, HttpClient client)
    {

        // Act
        var response = await client.GetAsync("/api/framework");
        response.EnsureSuccessStatusCode();
        var getFramework = await response.Content.ReadFromJsonAsync<Framework>();

        // Assert
        getFramework.ShouldNotBeNull();
        ArgumentNullException.ThrowIfNull(getFramework);
        getFramework.Id.ShouldBe(expectedFramework.Id);
        getFramework.Name.ShouldBe(expectedFramework.Name);

    }

    private async Task Update(Framework expectedFramework, HttpClient client)
    { 
        // Arrange
        string originalName = expectedFramework.Name;
        string updatedName = "Updated Framework";

        // Act
        expectedFramework.Name = updatedName;
        var response = await client.PutAsJsonAsync($"/api/framework/{expectedFramework.Id}", expectedFramework);
        response.EnsureSuccessStatusCode();
        var updatedFramework = await response.Content.ReadFromJsonAsync<Framework>();

        // Assert
        updatedFramework.ShouldNotBeNull();
        ArgumentNullException.ThrowIfNull(updatedFramework);
        updatedFramework.Id.ShouldBe(expectedFramework.Id);
        updatedFramework.Name.ShouldBe(updatedName);
    }

    private async Task Delete(Framework expectedFramework, HttpClient client)
    {
        // Act
        var response = await client.DeleteAsync($"/api/framework/{expectedFramework.Id}");
        response.EnsureSuccessStatusCode();
    }

    private async Task GetByIdNotFound(Framework expectedFramework, HttpClient client)
    {
        // Act
        var getResponse = await client.GetAsync($"/api/framework/{expectedFramework.Id}");

        // Assert
        getResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }

}
