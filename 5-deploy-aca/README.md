Deploy with the Aspire CLI to Azure Container Apps
==================================================

In this chapter, you'll publish Bicep files using the Aspire CLI that can deploy to Azure Container Apps (ACA).

Azure Container Apps (ACA) is a fully managed serverless container platform on Azure. It provides automatic scaling, built-in load balancing, secrets management, and seamless integration with other Azure services. ACA is ideal for microservices, APIs, web applications, and background jobs when managing Kubernetes YAML files is overwhelming.

Aspire can generate Azure Bicep files and deploy your application directly to Azure Container Apps, making it easy to deploy your entire application stack to the cloud.


Prerequisites
-------------

Before starting, ensure you have:

1. Ensure you've installed everything from [Chapter 0](../0-install/README.md)

2. Ensure your container runtime is started.

   Start Docker Desktop or Podman.

3. Select a project to deploy. You could choose:

   - The greenfield application from Chapter 2

   - The brownfield application from Chapter 3

   - A brand new project you create with File -> New Project

   - A solution you've created elsewhere


### Optional setup

1. Ensure you have an Azure Subscription.

   You can create a free trial: https://azure.microsoft.com/en-us/pricing/purchase-options/azure-account

2. Ensure you're logged into the Azure CLI:

   ```sh
   az login
   ```

3. Ensure the Azure Developer CLI (azd) is on your path:

   ```sh
   azd version
   ```


Generating Azure Bicep files with Aspire CLI
--------------------------------------------

See https://learn.microsoft.com/en-us/dotnet/aspire/deployment/aspire-deploy/aca-deployment-aspire-cli

1. Open your solution in Visual Studio, VS Code, or your favorite IDE.

2. To the `AppHost` project, add the NuGet package `Aspire.Hosting.Azure.AppContainers`.

3. At the top of `AppHost.cs` add this line:

   ```csharp
   builder.AddAzureContainerAppEnvironment("aca");
   ```

   Note that `aca` is just a name.  You could name it anything like `foo` or `project1`.

4. Remove all other publishing lines, all lines that include `.Add*Environment()`.

5. Open a terminal in the same directory as the solution file and run this:

   ```sh
   aspire publish -o dist
   ```

   You could adjust this to output to any folder.

   **Note**: As part of scaffolding the bicep files, it didn't build the containers nor push them to Azure Container Registry.  You may need to build the containers separately.  See `build-manual.sh` for inspiration.

6. Open the output folder - the `dist` folder and review the contents.

   Note the standard Bicep files:

   - `main.bicep` is the starting script. The parameters listed here will be important.

   - One folder for each other resource

   - `aca` folder creates an Azure Container Apps environment.

Yay!  We have Bicep files!


Modifying the Scaffolded Files
------------------------------

These file may not be exactly what you'd build.  But it's definitely a good start.  Depending on your needs, you may choose to move this folder to a convenient spot and alter it to suit your needs.

You may choose to:

- Add more whitespace.

- Delete services that you don't need.

- Remove any absolute paths in favor of relative paths.

- Add the Aspire dashboard container and add related OpenTelemetry settings.

- Centralize common configuration and secrets.

- Swap the reverse proxy from YARP to a Azure API Gateway.

- Swap the data stores from containers to PaaS services such as Postgres for Azure and Azure Redis Cache.

- Add custom domains by altering DNS names

- Use virtual networks for private connectivity


Optional: Deploy to Azure using the Aspire CLI
----------------------------------------------

If the Bicep files are exactly what you want or you want to go straight from the Aspire AppHost straight to Azure, you can let Aspire do this for you.

In the same directory as the solution file, run:

```sh
aspire deploy
```

This will generate intermediate Bicep files then directly deploy them to Azure.


Optional: Deploy the App Host to Azure using azd
------------------------------------------------

The Azure Developer CLI (azd) provides a simple path to deploy Aspire applications to Azure Container Apps.

1. Open a terminal in the same directory as your solution file.

2. Initialize azd in your solution directory:

   ```sh
   azd init
   ```

   - When prompted, select "Use code in the current directory"
   - Confirm the AppHost project detection
   - Choose an environment name (e.g., `dev`, `prod`)

3. Provision and deploy to Azure:

   ```sh
   azd up
   ```

   This command will:
   - Prompt you to select an Azure subscription
   - Prompt you to select an Azure region (e.g., `eastus`, `westus2`)
   - Create a resource group
   - Provision Azure Container Apps Environment
   - Provision Azure Container Registry
   - Build and push container images
   - Deploy your services to Azure Container Apps
   - Set up networking and environment variables

4. The command will output the URLs for your deployed services. Open them in a browser to verify deployment.

5. Monitor your deployment:

   ```sh
   azd monitor
   ```

   This opens Application Insights to view logs and metrics.


Optional: Deploy the Bicep files to Azure using the Azure CLI
-------------------------------------------------------------

If you've altered the Bicep files or want an intermediary step after generating them, you can deploy using the Azure CLI.

1. Open a terminal in the target directory (`dist`).

2. Deploy to Azure:

   ```sh
   az login
   az group create -n AspireAcaRG --location "eastus"
   az deployment group create -n AspireAca -g AspireAcaRG --template-file main.bicep
   ```

   Note: You may need to pass additional parameters based on your application's requirements.


Understanding the Deployed Resources
-----------------------------------

After deployment, your Azure resource group will contain:

- **Container Apps Environment**: The managed environment hosting your containers
- **Container Registry**: Stores your built container images
- **Container Apps**: One for each service in your Aspire application
- **Log Analytics Workspace**: For logging and monitoring
- **Application Insights**: For distributed tracing and telemetry
- **Additional Resources**: Any databases, storage accounts, or other dependencies defined in your AppHost

To view your resources in the CLI

```sh
az resource list -g AspireAcaRG -o table
```

Or open the AspireAcaRG resource group in the Azure Portal: https://portal.azure.com/


Managing Your Deployment
------------------------

### Viewing logs

```sh
# View logs for a specific Container App
az containerapp logs show -n myapi -g AspireAcaRG --follow

# Or use the Azure portal
az containerapp browse -n myapi -g AspireAcaRG
```

### Updating your application

After making code changes:

```sh
# With azd
azd deploy

# Or manually
aspire publish -o dist
az deployment group create -n AspireAca -g AspireAcaRG --template-file ./dist/main.bicep
```

### Viewing application metrics

```sh
azd monitor
```

Or visit the Azure Portal and navigate to Application Insights.

### Scaling your application

```sh
# Scale a Container App
az containerapp update -n myapi -g AspireAcaRG --min-replicas 2 --max-replicas 10
```

### Cleaning up resources

When you're done, delete the resource group to remove all resources:

```sh
# With azd
azd down

# Or manually
az group delete -n AspireAcaRG --yes
```


Troubleshooting
---------------

### Common Issues

**Deployment fails with "quota exceeded":**

- Check your Azure subscription limits
- Try a different region with available capacity
- Request quota increases in the Azure Portal

**Container Apps won't start:**

- Check logs with `az containerapp logs show`
- Verify environment variables and secrets are correctly configured
- Ensure container images were successfully pushed to ACR
- Check that startup probes and health checks are properly configured

**Services can't communicate:**

- Verify internal ingress is enabled for backend services
- Check that service-to-service URLs are correctly configured
- Review network policies if using a VNet

**Image pull failures:**

- Ensure the Container Apps environment has access to the Container Registry
- Verify managed identity or registry credentials are configured
- Check that images were successfully built and pushed

### Debugging Tips

1. **View Container App revision details:**

   ```sh
   az containerapp revision list -n myapi -g AspireAcaRG -o table
   ```

2. **Describe a specific Container App:**

   ```sh
   az containerapp show -n myapi -g AspireAcaRG
   ```

3. **Check Container Registry images:**

   ```sh
   az acr repository list -n myregistry -o table
   ```

4. **Stream logs in real-time:**

   ```sh
   az containerapp logs show -n myapi -g AspireAcaRG --follow --tail 50
   ```

5. **Access the Azure Portal for visual debugging:**

   ```sh
   az containerapp browse -n myapi -g AspireAcaRG
   ```


Additional Resources
-------------------

- [Azure Container Apps documentation](https://learn.microsoft.com/azure/container-apps/)
- [.NET Aspire Deployment](https://learn.microsoft.com/dotnet/aspire/deployment/)
- [Azure Developer CLI documentation](https://learn.microsoft.com/azure/developer/azure-developer-cli/)
- [Aspire and Azure Container Apps](https://learn.microsoft.com/dotnet/aspire/deployment/azure/aca-deployment)
- [Chapter 1 - Full Aspire Sample](../1-everything/README.md) for a complete example
