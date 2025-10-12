namespace AspireEverything.FrameworkApi.Tests.Fixtures;

internal class FrameworkApiApp(ITestOutputHelper testOutputHelper, string environment = "Development") : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(environment);

        builder.ConfigureServices(services =>
        {
            // Add mock/test services to the builder here

            builder.ConfigureLogging(logging =>
            {
                // https://www.meziantou.net/how-to-view-logs-from-ilogger-in-xunitdotnet.htm
                // https://blog.martincostello.com/writing-logs-to-xunit-test-output/
                services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(testOutputHelper));
            });

            /*
            var oldDb = services.FirstOrDefault(s => s.ServiceType == typeof(MyDbContext));
            if (oldDb != null)
            {
                services.Remove(oldDb);
            }

            services.AddDbContext<MyDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "MyDb"));
            */

        });

        return base.CreateHost(builder);
    }
}
