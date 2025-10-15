Aspire Brownfield App
=====================

This demo shows how to upgrade an existing app to use Aspire.

The `start` folder is before the upgrade.

The `done` folder is after the upgrade.

This app doesn't use as many Aspire components to make the upgrade experience simpler and avoid changing existing app paradigms.


Start
-----

Before the upgrade, launching the app is a bit involved.

1. Run `docker compose up` to start Redis.

2. Open `AspireBrownfield.sln` in Visual Studio or VS Code.

3. Configure user secrets to get the connection string just right.

4. In Visual Studio, in the Solution Explorer, right-click on the Solution, choose `Configure Startup Projects`, click `Multiple Projects` and set both projects to start.

5. Debug in Visual Studio or VS Code.


Upgrading
---------

1. Open `AspireBrownfield.sln` in Visual Studio.

2. Right-click on each project, choose `Add`, and then choose `.NET Aspire`.  Answer yes to the questions.

3. Right-click the other project and do the same.

4. Modify `AspireBrownfield.AppHost/AppHost.cs` to include:

   - a reference from the API to the Website
   - a Redis cache
   - a reference from Redis to both apps

   HACK: this needs very much more detail !!!

5. We don't need to modify the Redis connection string, but we do need to modify the config for the API url.  The setting is no longer in ~~`AppSettings:WeatherApi`~~.  It's now in `services:apiservice:https:0`.  Thanks service discovery!

   We can also delete these settings in appsettings.json and appsettings.Development.json.

6. Now that .NET Aspire launches the Redis container for us, we can delete `docker-compose.yaml`.


Done
----

After the upgrade, launching the app is super simple.

1. Open `AspireBrownfield.sln` in Visual Studio or VS Code.

2. Set `AspireBrownfield.AppHost` as the startup project.

3. Begin debugging.

The Redis connection string gets auto-magically injected into both apps, and the API URL gets injected too.
