var builder = DistributedApplication.CreateBuilder(args);

// from NuGet package `Aspire.Hosting.Redis`
var cache = builder.AddRedis("cache")
    .WithImageTag("alpine");

var apiservice = builder.AddProject<Projects.AspireBrownfield_ApiService>("apiservice")
    .WithReference(cache, "Redis") // named to match the connection string name
    .WaitFor(cache);

var web = builder.AddProject<Projects.AspireBrownfield_Web>("web")
    .WithReference(cache, "Redis")
    .WaitFor(cache)
    .WithReference(apiservice)
    .WaitFor(apiservice);

builder.Build().Run();
