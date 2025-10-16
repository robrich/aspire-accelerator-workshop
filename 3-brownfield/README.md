Aspire Brownfield App
=====================

This demo shows how to upgrade an existing .NET web app to use Aspire.

The `start` folder is before the upgrade.

The `done` folder is after the upgrade.

This app doesn't use as many Aspire components to make the upgrade experience simpler and avoid changing existing app paradigms.  We're intentionally modifying existing code as little as possible to avoid breaking existing workflows and conventions.

We'll show how we can bend Aspire's app host to pass the connection strings the way the app uses them today.  We'll also show how we must bend the app's service discovery to match the Aspire conventions.


Running the Solution the old way
--------------------------------

Before the upgrade, launching the app is a bit involved.

1. Run `docker compose up` to start Redis.

2. Open `AspireBrownfield.sln` in Visual Studio or VS Code.

3. Configure user secrets to get the connection string just right.

4. In Visual Studio, in the Solution Explorer, right-click on the Solution, choose `Configure Startup Projects`, click `Multiple Projects` and set both projects to start.

5. Debug in Visual Studio or VS Code.


Upgrading the Solution
----------------------

1. Open `AspireBrownfield.sln` in Visual Studio.

2. Right-click on one of the existing projects, choose `Add`, and then choose `.NET Aspire`.  Answer yes to the questions.

   This adds the AppHost and ServiceDefaults projects.

3. Right-click the other existing project and do the same.

   This modifies the AppHost to reference this project too, and modifies the project to leverage ServiceDefault's best practices including OpenTelemetry setup, service health, and HttpClient retries.

4. In the AppHost project, add Redis:

   a. Manage NuGet packages for the AppHost project, and add this new package: `Aspire.Hosting.Redis`

   b. Add this code to the top of AppHost.cs:

      ```c#
      var cache = builder.AddRedis("cache")
        .WithImageTag("alpine");
      ```

5. Modify the API project to reference Redis:

   a. Open `ApiService/appsettings.json`.  Notice that the connection string is named `Redis`.

   b. In `AppHost.cs`, modify the ApiService to look like this:

      ```c#
      var apiservice = builder.AddProject<Projects.AspireBrownfield_ApiService>("apiservice")
        .WithReference(cache).WaitFor(cache);
      ```

   c. If we left it there, the app would need to listen for a connection string named `cache` - matching the name we passed into `AddRedis()`.  But the connection string is named `Redis`.

   d. We could change either one, but let's leave the app untouched as much as possible.  Let's choose to change `AppHost.cs`:

      Modify the ApiService reference to look like this:

      ```c#
      var apiservice = builder.AddProject<Projects.AspireBrownfield_ApiService>("apiservice")
        .WithReference(cache, "Redis").WaitFor(cache);
      ```

      With the second parameter to `.WithReference()` we're now specifically naming the connection string environment variable.

   **Note**: We could change the connection string name in the AppHost project, minimizing the changes to the existing application.

6. Modify the Web project reference to reference Redis:

   Like we did for the API project, let's add a reference to the Redis cache with a named connection string:

   ```c#
   var web = builder.AddProject<Projects.AspireBrownfield_Web>("web")
     .WithReference(cache, "Redis").WaitFor(cache);
   ```

7. Modify the Web project to reference the API project for service discovery.

   In AppHost.cs, change the web definition to this:

   ```c#
   var web = builder.AddProject<Projects.AspireBrownfield_Web>("web")
     .WithReference(cache, "Redis").WaitFor(cache)
     .WithReference(apiservice).WaitFor(apiservice); // <-- add this line
   ```

   Now Aspire will add an environment variable with the randomly selected port for the API project.

8. The Web project currently looks to `AppSettings:WeatherApi` for the URL to the API project.  We need to modify the app to use the newly injected environment variable from Aspire.

   a. Open `AspireBrownfield.Web/Program.cs`

   b. Modify the line that gets the API's url:

      ~~`string? apiUrl = builder.Configuration.GetValue<string>("AppSettings:WeatherApiUrl");`~~

      becomes

      ```c#
      string? apiUrl = builder.Configuration.GetValue<string>("services:apiservice:https:0");
      ```

      Note how we're using the name `apiservice` that we set in `.AddProject<T>("apiservice")`.

   **Note**: In this case, we can't change the Aspire service discovery convention, so we must change the application.

9. In the deployed environment, we'll also need to change the way this configuration is set.  Do we need to change the Dockerfile?  Kubernetes YAML?  Azure WebApp settings?  DevOps pipelines?  Automated tests?  You'll need to carefully review the build and deploy steps to also leverage this new convention.

   Note that environment variables generally can't use `:` so they use `__` instead.  So the environment variable injected in from Aspire - and the variable you'd set in Kubernetes / Azure WebApp / etc is `services__apiservice__https__0`.

   Since this workshop isn't deployed anywhere yet, there's nothing to do here.  Were this a production deployed app, we'd need to work carefully to execute this task.

10. Optional clean-up: Remove `AppSettings:WeatherApi`:

    In the Web project, in both `appsettings.json` and `appsettings.Development.json` we no longer need the `WeatherApi` reference.

    You can delete the `WeatherApi` line from both files.

11. Optional clean-up: Remove `docker-compose.yaml`

    Now that we use Aspire to start the Redis container, we no longer need the docker-compose.yaml file.

    You can delete `docker-compose.yaml`.

Excellent!  All the changes are made.

Note that we could change Aspire's connection string name, so we didn't need to change the app.

Note that we could not change Aspire's service discovery convention, so we needed to change the app's methodology of connecting from one service to another.


Running the Solution the new way
--------------------------------

After the upgrade, launching the app is super simple.

1. Open `AspireBrownfield.sln` in Visual Studio or VS Code.

2. Set `AspireBrownfield.AppHost` as the startup project.

3. Begin debugging.

The Redis connection string gets auto-magically injected into both apps, and the API URL gets injected too.
