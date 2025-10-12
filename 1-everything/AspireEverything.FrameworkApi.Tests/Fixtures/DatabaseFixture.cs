namespace AspireEverything.FrameworkApi.Tests.Fixtures;

public static class DatabaseFixture
{

    public static async Task ResetDatabase()
    {
        string conn = ConfigurationFixture.GetConnectionString("voting");
        if (conn.Contains("database.windows.net"))
        {
            throw new Exception("Please don't reset a production database");
        }
        Respawner respawner = await Respawner.CreateAsync(conn);
        await respawner.ResetAsync(conn);
    }

}
