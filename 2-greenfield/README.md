Aspire Greenfield App
=====================

This demo shows how to create a new Aspire app from the sample.


One-time Setup
--------------

See also the [.NET Aspire install instructions](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/setup-tooling?tabs=windows&pivots=visual-studio#install-net-aspire) from Microsoft.

### Containers

You'll need either Docker Desktop or Podman installed to use any containers.  The .NET Aspire starter project uses a Redis container as an output cache.

### Visual Studio 2022

1. Start the Visual Studio 2022 Installer.

2. Click `Modify`.

3. Switch to `Individual Components`.

4. Ensure `.NET Aspire` is checked.

   ![.NET Aspire install in Visual Studio 2022](https://learn.microsoft.com/en-us/dotnet/aspire/docs/media/install-aspire-workload-visual-studio.png)

5. Open a terminal in any directory.

6. Run `dotnet new update` to update the Aspire project templates.

### VS Code

1. Install .NET SDK v9.0 or later.

2. Open a terminal in any directory.

3. Run `dotnet new install Aspire.ProjectTemplates` to install the Aspire project templates.

4. Run `dotnet new update` to update the Aspire and all other new project templates.


New Project
-----------

See also the [Aspire documentation](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/build-your-first-aspire-app) from Microsoft.

### Visual Studio 2022

1. File -> New Project.

2. Choose the `.NET Aspire Starter Application` from the list.

3. If you don't have a container runtime, **uncheck** `use redis cache`.

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

HACK: teach them how to use the Aspire dashboard !!!

- each project's console is now in the Aspire dashboard
