Deploy with the Aspire CLI to Kubernetes
========================================

In this chapter, you'll deploy a solution to a Kubernetes Helm chart using Aspire.

Kubernetes is a powerful orchestrator for running containers in production-like environments. It provides built-in primitives for scaling, networking, secrets, and health checks. For local experimentation, we'll use a local cluster (Docker Desktop, Podman, kind, minikube, etc). For production, use a cloud service like AKS, EKS, GKE, or similar.

Aspire can generate a Helm chart that makes it easy to deploy your Aspire application to Kubernetes.


Prerequisites
-------------

Before starting, ensure you have:

1. Ensure you've installed everything from [Chapter 0](../0-install/README.md)

2. Ensure your container runtime is started.

   Start Docker Desktop or Podman.

3. Ensure you've started Kubernetes mode and Helm is in your path.

   ```sh
   kubectl version
   helm version
   ```

4. Select a project to deploy.  You could choose:

   - The greenfield application from Chapter 2

   - The brownfield application from Chapter 3

   - A brand new project you create with File -> New Project

   - A solution you've created elsewhere


Generating a Kubernetes Helm chart with Aspire CLI
--------------------------------------------------

See https://github.com/dotnet/aspire/tree/main/src/Aspire.Hosting.Kubernetes

1. Open your solution in Visual Studio, VS Code, or your favorite IDE.

2. To the `AppHost` project, add the NuGet package `Aspire.Hosting.Kubernetes`.

3. At the top of `AppHost.cs` add this line:

   ```csharp
   builder.AddKubernetesEnvironment("k8s");
   ```

   Note that `k8s` is just a name.  You could name it anything like `foo` or `project1`.

4. Remove all other publishing lines, all lines that include `.Add*Environment()`.

5. Open a terminal in the same directory as the solution file and run this:

   ```sh
   aspire publish -o dist
   ```

   You could adjust this to output to any folder.

   **Note**: As part of scaffolding the Helm chart, it didn't build the containers.  You may need to build the containers separately.  See `build-manual.sh` for inspiration.

6. Open the output folder - the `dist` folder and review the contents.

   Note the standard Helm chart details including `Chart.yaml` and  it created `docker-compose.yaml` and possibly a `.env` file with secrets.

   Depending on the dependencies in your application, it might have also created `main.bicep` and other folders for each Azure resource.  For example, if you included an Azure function, it'll create the Bicep to initialize Azure Storage.

Yay!  We have a Helm chart!


Modifying the Scaffolded Files
------------------------------

These file may not be exactly what you'd build.  But it's definitely a good start.  Depending on your needs, you may choose to move this folder to a convenient spot and alter it to suit your needs.

You may choose to:

- Add more whitespace.

- Swap service types from `ClusterIP` to `NodePort` where you can connect to them running in a local cluster.

- Remove any absolute paths in favor of relative paths.

- Add the Aspire dashboard container and add related OpenTelemetry settings.

- Centralize the ConfigMaps and secrets.

- Swap the reverse proxy from YARP to a Kubernetes ingress.

- Swap the data stores from containers to PaaS services such as Postgres for Azure and Azure Redis Cache.

- Simplify values.yaml to only those settings that you care to change.


Optional: Deploy to Azure using the Aspire CLI
----------------------------------------------

If the Helm chart is exactly what you want or you want to go straight from the Aspire AppHost straight to Azure, you can let Aspire do this for you.

In the same directory as the solution file, run:

```sh
aspire deploy
```

This will generate intermediate Helm chart then directly deploy it to Azure.


Optional: Deploy the Helm chart to Kubernetes
---------------------------------------------

1. Optional: adjust any data in `values.yaml`.

2. Open a terminal in the target folder (`dist`).

3. Optional: deploy Azure resources.

   Did your solution create a `main.bicep` file and other Bicep resources?

   ```sh
   az login
   az group create -n AspireK8sGroup --location "Central US"
   az deployment group create -n AspireK8s -g AspireK8sGroup --template-file main.bicep --parameters ...
   ```

4. Build the containers.

   **Note**: As part of scaffolding the Helm chart, it didn't build the containers.

   Run the necessary build commands to build the containers.  See `build-manual.sh` for inspiration.

   OR

   Complete the Docker Compose tutorial first.  The Aspire publish to Docker Compose does build the containers using the .NET SDK or applicable Dockerfiles.

5. Deploy the Helm chart.

   ```sh
   helm template . | kubectl apply -f -
   ```

   To override values on the command line, you can add `--set` parameters e.g.:

   ```sh
   helm upgrade --install aspire . --set acrUrl="myregistry.azurecr.io" --set imageVersion="latest"
   ```

6. Verify resources are created and healthy:

   ```sh
   kubectl get all
   ```

7. Get the exposed port(s) from Kubernetes:

   ```sh
   kubectl get svc
   ```

8. Open a browser and browse to the NodePorts of exposed services.

9. When you're done, uninstall the Helm chart:

   ```
   helm template . | kubectl delete -f -
   ```

   OR

   delete the deployments, services, ConfigMaps, secrets, and other resources using kubectl commands.


Troubleshooting
---------------

### Common issues

**Pods stuck in ImagePullBackOff:**

- Ensure you've built the images and optionally pushed them to your container registry
- If private, configure image pull secrets and reference them in your chart or cluster
- Make images public temporarily for testing

**Services not reachable:**

- Check Service types and ports; the chart uses ClusterIP by default, so you may need to adjust to NodePort to connect
- Verify target container ports match your app configuration; e.g. Web connects to the correct API port

**General debugging tips:**

1. Check events and pod statuses:

   ```sh
   kubectl get events --sort-by=.lastTimestamp
   kubectl describe pod <pod-name>
   ```

2. Tail logs:

   ```sh
   kubectl logs deployment/web -f
   ```

3. Port-forward a Service for quick testing (bypassing Ingress):

   ```sh
   kubectl port-forward svc/web 8080:8080
   ```


Additional resources
--------------------

- [Kubernetes documentation](https://kubernetes.io/docs/home/)
- [Helm documentation](https://helm.sh/docs/)
- [.NET Aspire Deployment](https://learn.microsoft.com/dotnet/aspire/deployment/)
- [Chapter 1 - Full Aspire Sample](../1-everything/README.md) for a complete example
