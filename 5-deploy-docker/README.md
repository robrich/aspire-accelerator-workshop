Deploy with the Aspire CLI to Docker Compose
============================================

In this chapter, you'll publish a Docker Compose file using the Aspire CLI.

Docker Compose is a great tool for local development, allowing you to start dependent applications in containers so you can focus on a specific application you want to develop.  For example, you could start the backend and data stores and focus on developing the React application - even if you don't have .NET installed.

Aspire can generate a `docker-compose.yaml` file from your AppHost configuration, making it easy to deploy your entire application stack to Docker.


Prerequisites
-------------

Before starting, ensure you have:

1. Ensure you've installed everything from [Chapter 0](../0-install/README.md)

2. Ensure your container runtime is started.

   Start Docker Desktop or Podman.

3. Select a project to deploy.  You could choose:

   - The greenfield application from Chapter 2

   - The brownfield application from Chapter 3

   - A brand new project you create with File -> New Project

   - A solution you've created elsewhere


Generating Docker Compose Files with Aspire CLI
-----------------------------------------------

See https://github.com/dotnet/aspire/tree/main/src/Aspire.Hosting.Docker

1. Open your solution in Visual Studio, VS Code, or your favorite IDE.

2. To the `AppHost` project, add the NuGet package `Aspire.Hosting.Docker`.

3. At the top of `AppHost.cs` add this line:

   ```csharp
   builder.AddDockerComposeEnvironment("compose");
   ```

   Note that `compose` is just a name.  You could name it anything like `foo` or `project1`.

4. Remove all other publishing lines, all lines that include `.Add*Environment()`.

5. Open a terminal in the same directory as the solution file and run this:

   ```sh
   aspire publish -o dist
   ```

   You could adjust this to output to any folder.

   **Note**: As part of scaffolding the Docker Compose file, it also built all the containers using the .NET SDK or the Dockerfiles specified.

6. Open the output folder - the `dist` folder and review the contents.

   Note that it created `docker-compose.yaml` and possibly a `.env` file with secrets.

   Depending on the dependencies in your application, it might have also created `main.bicep` and other folders for each Azure resource.  For example, if you included an Azure function, it'll create the Bicep to initialize Azure Storage.

Yay!  We have a `docker-compose.yaml` file!


Modifying the Scaffolded Files
------------------------------

This file may not be exactly what you'd build.  But it's definitely a good start.  Depending on your needs, you may choose to move this file to a convenient spot and alter it to suit your needs.

You may choose to:

- Add more whitespace.

- Remove any absolute paths in favor of relative paths.

- Change `expose` lines to `ports` lines.  `expose` exposes the port to other containers in this network. `ports` exposes it outside the Docker network to you too.

- Remove the Aspire dashboard container and remove related OpenTelemetry settings.


Optional: Start the created `docker-compose.yaml` file
------------------------------------------------------

1. In the `dist` folder, read through the `docker-compose.yaml` file and find the `ports` sections, noting the ports you'll use for each resource.

2. Optional: adjust any configuration details in `dist/.env`.

3. Open a terminal in the target directory (`dist`).

4. Optional: deploy Azure resources.

   Did your solution create a `main.bicep` file and other Bicep resources?

   ```sh
   az login
   az group create -n AspireDockerGroup --location "Central US"
   az deployment group create -n AspireDocker -g AspireDockerGroup --template-file main.bicep --parameters ...
   ```

5. Run the Docker Compose file.

   ```sh
   docker compose up
   ```

   If you'd like, add `-d` to run in daemon or detached mode.

6. Open a browser to each port identified in step 1 above and run the application.

7. When you're done, stop the application:

   ```sh
   docker compose down
   ```


Troubleshooting
---------------

### Common Issues

**Containers won't start:**

- Check if ports are already in use:

  On Windows:

  ```powershell
  netstat -ano | findstr :8080
  ```

  On Linux:

  ```sh
  netstat -tuln | grep :8080
  ```

- Stop conflicting services or change the port in docker-compose.yaml

**Database connection failures:**

- Ensure the database container is healthy:

  ```sh
  docker compose ps
  ```

- Check connection strings in environment variables

- Wait for database initialization to complete (check logs)

### Tips

1. **Use `docker compose up` without `-d`** to see all logs in real-time

2. **Check service health** with `docker compose ps`

3. **Review environment variables** with `docker compose config`


Additional Resources
-------------------

- [Docker Compose documentation](https://docs.docker.com/compose/)
- [.NET Aspire Deployment](https://learn.microsoft.com/dotnet/aspire/deployment/)
- [Docker Best Practices](https://docs.docker.com/develop/dev-best-practices/)
- [Chapter 1 - Full Aspire Sample](../1-everything/README.md) for a complete example
