
var builder = DistributedApplication.CreateBuilder(args);

var sqlUsername = builder.AddParameter("postgresql-username", secret: true);
var sqlPassword = builder.AddParameter("postgresql-password", secret: true);
var dbName = "voting";
var postgres = builder.AddPostgres("postgres", userName: sqlUsername, password: sqlPassword)
	.WithImageTag("alpine")
	.WithEnvironment("POSTGRES_DB", dbName)
    .WithBindMount("../postgres-init", "/docker-entrypoint-initdb.d")
    .WithDataVolume("pg-data") // New data on each run
    //.WithDataBindMount("../pg-data") // Persist data between runs FRAGILE: postgres is really picky about WSL folder permissions
    .WithPgWeb(c => c.WithImageTag("latest"));

var postgresdb = postgres.AddDatabase(dbName);

var cache = builder.AddRedis("cache")
	.WithImageTag("alpine")
	.WithRedisCommander();

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
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints(); // If you're using it directly. If you're using the gateway, you can remove ExternalHttpEndpoints

var frontendReact = builder.AddNpmApp("frontendReact", "../AspireEverything.WebReact", "dev")
    .WithReference(frameworkApi).WaitFor(frameworkApi)
    .WithReference(funcVoteGet).WaitFor(funcVoteGet)
    .WithReference(funcVoteScore).WaitFor(funcVoteScore)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints(); // If you're using it directly. If you're using the gateway, you can remove ExternalHttpEndpoints

var gateway = builder.AddYarp("gateway")
    .WithHostPort(8080)
    .WithConfiguration(yarp =>
    {
        yarp.AddRoute("/api/vote/get", funcVoteGet);
        yarp.AddRoute("/api/vote/score/{**catch-all}", funcVoteScore);

        yarp.AddRoute("/api/{**catch-all}", frameworkApi); // technically only /api/framework

        yarp.AddRoute(frontendVue); // or frontendReact
    });

builder.Build().Run();
