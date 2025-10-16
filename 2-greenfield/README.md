Aspire Greenfield App
=====================

This demo shows how to create a new Aspire app from the sample.


New Project
-----------

See also the [Aspire documentation](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/build-your-first-aspire-app) from Microsoft.

### Visual Studio 2022

1. File -> New Project.

2. Choose the `.NET Aspire Starter Application` from the list.

3. Check the option to `use redis cache`.

4. Pick all other options that make sense to you.

5. Start debugging.

### VS Code

1. Open VS Code.

2. Close any open folder or project or solution.

3. Click the `Create .NET project` button.

   ![VS Code start .NET Aspire project](https://learn.microsoft.com/en-us/dotnet/aspire/docs/includes/media/vscode-new-starter-project.png)

4. Choose the `.NET Aspire Starter Application` from the list.

5. Start debugging.

### Terminal

1. Open a terminal in any empty folder

2. Run `dotnet new aspire-starter --use-redis-cache`


Aspire Dashboard
----------------

Notice these features of the Aspire dashboard:

1. In the Resources tab, you can click each resource to see the details including environment variables and other configuration.

2. In the Resources tab, you can switch between a list and a dependency graph.

3. In the Console view you can pick the resource from the list at the top and see the full console logs for each project or service.  No more flipping between endless console windows to figure out what happened.

4. In the Structured Logs view, the console logs are parsed, and errors are highlighted.

5. In the Traces view, we see how one service calls another, allowing us to watch network calls flow through the system.
