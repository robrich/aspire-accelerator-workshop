# Aspire Workshop Glossary

A quick reference for key terms, concepts, and components used in the Aspire Workshop and related demos.

## .NET Workload

A set of optional SDK components for .NET, such as Aspire, Web, or Mobile workloads, installed via `dotnet workload install`.

## AddParameter (method)

A method to inject configuration values or secrets into a service at startup, often from environment variables or secret stores.

## ACA (Azure Container Apps)

A managed Azure service for running containerized applications and microservices, supporting scaling and integrated monitoring.

## ACR (Azure Container Registry)

An Azure version of Docker Hub for storing container images from Docker to run in Kubernetes or another container runtime. Where Docker Hub defaults to making the images public, Azure Container Registry defaults to making the images private. You can configure Docker Hub to make images private, and if using an advanced tier you can make ACR images public.

## ACS (Azure Container Service)

A legacy Azure service for orchestrating containers, often replaced by ACA (Azure Container Apps) or AKS (Azure Kubernetes Service).

## AKS (Azure Kubernetes Service)

A managed Kubernetes service from Azure for deploying, managing, and scaling containerized applications using Kubernetes clusters and tools. Suitable for production-grade orchestration, scaling, and integration with Azure networking and identity services.

## App Host

The main entry point for an Aspire application. Orchestrates service startup, configuration, and dependency injection for all projects in the solution.

## Azure Functions

A serverless compute service for running event-driven code in Azure, often integrated with Aspire for background tasks or APIs.

## Blazor

A .NET web framework for building interactive client-side web UIs with C#, comparable to React and Vue.js.

## Docker Compose

A tool for defining and running multi-container Docker applications locally, often used for local Aspire development and testing.

## Docker Desktop

A developer tool for building and running containers. It is free for personal use and for business use up to a certain business size. Docker Desktop is definitely the easy button compared to other container building tools.

## Environment Variables

Configuration values set outside the application, used for secrets, connection strings, and runtime settings in Aspire.

## Key Vault

Azure's cloud service for securely storing and accessing secrets, keys, and certificates, often used in Aspire deployments.

## Mermaid Diagrams

A syntax for generating diagrams and flowcharts from text, used in documentation and architectural overviews.

## NPM Frontend

A frontend application built in JavaScript using Node.js and managed with npm (Node Package Manager), such as React or Vue.js.

## Output Caching

A server-side caching technique in ASP.NET that stores the entire generated response (HTML, JSON, etc.) for instant reuse, reducing load and latency. Different from response caching, which is client/proxy-side.

## Podman

An alternative to Docker Desktop for running containers. If Docker Desktop's licensing is getting in the way, Podman is a near drop-in replacement.

## Redis

An in-memory data store used for caching, messaging, and session storage. .NET can use Redis for output caching and other features.

## Response Caching

Caching based on HTTP headers, typically at the client or proxy level. Less effective for dynamic content than output caching.

## Service Defaults

A .NET shared project containing common configuration, extensions, and reusable code for Aspire services. Promotes consistency and DRY principles across microservices.

## Service Discovery

The mechanism by which Aspire services locate and communicate with each other, often using configuration, environment variables, or a discovery service.

## WaitFor (method)

A method or configuration pattern to delay service startup until a dependent service is available and healthy.

## Winget

A Windows package manager used to install tools like Azure Functions Core Tools.

## WithHttpHealthCheck (method)

A method to add HTTP-based health checks to a service, enabling Aspire to monitor and report service health.

## WithReference (method)

A method or configuration pattern in Aspire to declare dependencies between services, ensuring correct startup order and wiring.

## YARP

"Yet Another Reverse Proxy" – a .NET library for building reverse proxies and API gateways, often used in Aspire for routing and aggregation.
