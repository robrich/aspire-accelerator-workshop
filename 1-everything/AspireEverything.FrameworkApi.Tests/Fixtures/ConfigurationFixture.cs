namespace AspireEverything.FrameworkApi.Tests.Fixtures;

public class NotStatic { }
public static class ConfigurationFixture
{
    private static IConfigurationRoot? config = null;
    private static object configLock = new object();

    public static IConfigurationRoot GetConfiguration()
    {
        if (config == null)
        {
            lock (configLock)
            {
                if (config == null)
                {
                    string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
                    config = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                        .AddUserSecrets<NotStatic>()
                        .AddEnvironmentVariables()
                        .Build()!;
                }
            }
        }
        return config;
    }

    public static string GetConnectionString(string connectionStringName)
    {
        IConfigurationRoot config = GetConfiguration();
        string? connectionString = config.GetConnectionString(connectionStringName);
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException(nameof(connectionString));
        }
        return connectionString;
    }

}
