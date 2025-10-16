
var builder = DistributedApplication.CreateBuilder(args);

// When publishing, you run:
// `aspire publish -o dist`
// There is no flag saying where you'd like to publish to
// Therefore you specify where you want to publish here:
string deployTo = "none";
switch (deployTo)
{
    case "docker-compose":
        // See https://www.nuget.org/packages/Aspire.Hosting.Docker
        builder.AddDockerComposeEnvironment("compose");
        break;
    case "kubernetes":
        // See https://www.nuget.org/packages/Aspire.Hosting.Kubernetes
        builder.AddKubernetesEnvironment("k8s");
        break;
    case "azure-container-apps":
        // See https://learn.microsoft.com/en-us/dotnet/aspire/deployment/aspire-deploy/aca-deployment-aspire-cli
        builder.AddAzureContainerAppEnvironment("aca");
        break;
    case "none":
        // when running locally with `aspire run` it just creates a few nearly empty bicep files
        break;
    default:
        throw new ArgumentOutOfRangeException("Don't know how to deploy to " + deployTo);
}

var sqlUsername = builder.AddParameter("postgresql-username", secret: true);
var sqlPassword = builder.AddParameter("postgresql-password", secret: true);
var dbName = "voting";
var postgres = builder.AddPostgres("postgres", userName: sqlUsername, password: sqlPassword)
	.WithImageTag("alpine")
	.WithEnvironment("POSTGRES_DB", dbName)
    .WithDataVolume("pg-data") // New data on each run
    //.WithDataBindMount("../pg-data") // Persist data between runs FRAGILE: postgres is really picky about WSL folder permissions
    .WithPgWeb(c => c.WithImageTag("latest"));
if (deployTo != "kubernetes")
{
    // aspire publish to k8s doesn't know how to do bind mounts
    // see https://github.com/dotnet/aspire/issues/11267
    postgres.WithBindMount("../postgres-init", "/docker-entrypoint-initdb.d");
}

var postgresdb = postgres.AddDatabase(dbName);

var cache = builder.AddRedis("cache")
	.WithImageTag("alpine")
	.WithRedisCommander();
if (deployTo != "kubernetes")
{
    // aspire publish to k8s doesn't know how to do bind mounts
    // see https://github.com/dotnet/aspire/issues/11267
    cache.WithDataBindMount("../redis-data");
}

var frameworkApi = builder.AddProject<Projects.AspireEverything_FrameworkApi>("frameworkapi")
    .WithReference(postgresdb).WaitFor(postgres)
    .WithReference(cache).WaitFor(cache)
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints(); // If you're using it directly. If you're using the gateway, you can remove ExternalHttpEndpoints

var funcVoteGet = builder.AddAzureFunctionsProject<Projects.AspireEverything_VoteGet>("funcVoteGet")
    .WithReference(postgresdb).WaitFor(postgres)
    .WithReference(cache).WaitFor(cache)
    .WithExternalHttpEndpoints(); // If you're using it directly. If you're using the gateway, you can remove ExternalHttpEndpoints

var funcVoteScore = builder.AddAzureFunctionsProject<Projects.AspireEverything_VoteScore>("funcVoteScore")
    .WithReference(postgresdb).WaitFor(postgres)
    .WithExternalHttpEndpoints(); // If you're using it directly. If you're using the gateway, you can remove ExternalHttpEndpoints


var frontendVue = builder.AddNpmApp("frontendVue", "../AspireEverything.WebVue", "dev")
    .WithReference(frameworkApi).WaitFor(frameworkApi)
    .WithReference(funcVoteGet).WaitFor(funcVoteGet)
    .WithReference(funcVoteScore).WaitFor(funcVoteScore)
    .WithExternalHttpEndpoints() // If you're using it directly. If you're using the gateway, you can remove ExternalHttpEndpoints
    .WithHttpEndpoint(env: "PORT", targetPort: 80)
    .PublishAsDockerFile();

var frontendReact = builder.AddNpmApp("frontendReact", "../AspireEverything.WebReact", "dev")
    .WithReference(frameworkApi).WaitFor(frameworkApi)
    .WithReference(funcVoteGet).WaitFor(funcVoteGet)
    .WithReference(funcVoteScore).WaitFor(funcVoteScore)
    .WithExternalHttpEndpoints() // If you're using it directly. If you're using the gateway, you can remove ExternalHttpEndpoints
    .WithHttpEndpoint(env: "PORT", targetPort: 80)
    .PublishAsDockerFile();


if (deployTo != "docker-compose")
{
    // FRAGILE: Can't use this directly. Must use it through the proxy.
    // Aspire doesn't load a Blazor WebAssembly using `dotnet run` so proxy.config.json doesn't work
    // see https://github.com/dotnet/aspire/issues/7524
    var frontendBlazor = builder.AddProject<Projects.AspireEverything_WebBlazor>("frontendBlazor")
        .WithReference(frameworkApi).WaitFor(frameworkApi)
        .WithReference(funcVoteGet).WaitFor(funcVoteGet)
        .WithReference(funcVoteScore).WaitFor(funcVoteScore)
        .WithExternalHttpEndpoints(); // If you're using it directly. If you're using the gateway, you can remove ExternalHttpEndpoints
        //.PublishAsDockerFile(); // PublishAsDockerfile only works for executables, not for projects.

    // FRAGILE: aspire publish doesn't use the correct base image for hosting the static assets
    // see https://github.com/dotnet/aspire/issues/6781
}

int gatewayPort = deployTo == "azure-container-apps" ? 80 : 8080; // ACA needs to be on port 80
var gateway = builder.AddYarp("gateway")
    .WithHostPort(gatewayPort)
    .WithConfiguration(yarp =>
    {
        yarp.AddRoute("/api/vote/get", funcVoteGet);
        yarp.AddRoute("/api/vote/score/{**catch-all}", funcVoteScore);

        yarp.AddRoute("/api/{**catch-all}", frameworkApi); // technically only /api/framework

        yarp.AddRoute(frontendReact); // or frontendReact or frontendVue or frontendBlazor
    });

builder.Build().Run();
