
var builder = FunctionsApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Fix swagger.json url in swagger-ui
var hostname = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");
builder.Services.AddSingleton<IOpenApiConfigurationOptions>(_ => new CustomOpenApiConfigurationOptions($"http://{hostname}/api"));

builder.ConfigureFunctionsWebApplication();

builder.Services.AddDbContext<VoteApiContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("voting")));

builder.AddRedisOutputCache("cache");

builder.Services.AddScoped<IVoteRepository, VoteRepository>();
builder.Services.AddScoped<IVoteApiContext>(provider => provider.GetRequiredService<VoteApiContext>());

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();


public class CustomOpenApiConfigurationOptions(string url) : OpenApiConfigurationOptions
{
    public override List<OpenApiServer> Servers => new List<OpenApiServer> { new OpenApiServer { Url = url } };
}
