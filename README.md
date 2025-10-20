Aspire Accelerator: Fast-Track to Cloud-Native Development
==========================================================

This is a comprehensive Aspire workshop to get you up-and-running with both development time and deployment skills. Learn how to build, orchestrate, and deploy cloud-native applications using Aspire.

![Rob and Barry teach Aspire](slides/img/rob-and-barry-presenting.jpg)


What is Aspire?
---------------

.NET Aspire is an opinionated, cloud-ready stack for building observable, production-ready, distributed applications. Aspire simplifies the development of cloud-native apps by providing:

- **Service Orchestration**: Start all your services (APIs, databases, message queues) with one click
- **Service Discovery**: Services automatically find each other without hard-coded URLs
- **Telemetry**: Built-in OpenTelemetry for logs, traces, and metrics
- **Deployment**: Generate deployment artifacts for Docker Compose, Kubernetes, and Azure Container Apps
- **Developer Dashboard**: A helpful UI showing all your services, logs, traces, and health checks


Before you Arrive
-----------------

Head to [Chapter 0](0-install/README.md) to set up your development environment.


About this Workshop
-------------------

This workshop provides hands-on experience with Aspire through a series of progressive tutorials:

### [Chapter 0: Installation](0-install/README.md)

Install all prerequisites including .NET 9 SDK, Aspire templates, Azure Functions Core Tools, Node.js, and a container runtime (Docker or Podman).

### [Chapter 1: Everything Sample](1-everything/README.md)

A comprehensive example demonstrating the full power of Aspire:

- **Multiple frontends**: React, Vue.js, and Blazor WebAssembly
- **Multiple backends**: .NET APIs and Azure Functions
- **Data stores**: Postgres and Redis with admin tools (PgWeb, Redis Commander)
- **Reverse proxies**: YARP, Vite (dev), and Kubernetes Ingress
- **Deployment options**: Docker Compose, Kubernetes & Helm, and Azure Container Apps (ACA)

This is a feature-rich reference implementation showing many patterns you can pick from for your own projects.  The project includes multiple implementations of each feature to add variety to your learning journey.  In a production system, you would likely choose a single technology in each space and delete the others.

### [Chapter 2: Greenfield App](2-greenfield/README.md)

Get your feet wet by building a new Aspire application and exploring the Aspire dashboard:

Create a brand new Aspire application from scratch using:

- Visual Studio's new project wizard
- VS Code's .NET project creator
- Command-line `dotnet new` templates

Then learn to navigate the Aspire Dashboard.  Finally understand the projects added by the Aspire templates.

### [Chapter 3: Brownfield App](3-brownfield/README.md)

Aspire isn't just for new projects.  You can also easily add Aspire to an existing .NET solution.  Learn how to:

- Add AppHost and ServiceDefaults to existing solutions
- Configure connection strings and service discovery
- Minimize changes to existing code
- Replace manual orchestration (docker-compose) with Aspire

### [Chapter 4: Advanced Aspire](4-aspire-advanced/README.md)

Go beyond the basics with advanced integrations:

- Add Postgres with initialization scripts and PgWeb admin
- Configure parameters and secrets in Aspire
- Integrate Azure Functions into orchestration
- Add Node.js frontend projects (React, Vue, etc.)
- Set up reverse proxies (Vite: dev or YARP)
- Explore the broader Aspire ecosystem and Community Toolkit

### Chapter 5: Deployment

There are 3 deployment options built into Aspire.

(5-deploy-aca/README.md)

- [**Docker Compose**](./5-deploy-docker/README.md): Prop up other dependencies during development
- [**Azure Container Apps**](./5-deploy-aca/README.md): ACA is great when Kubernetes YAML is too confusing
- [**Kubernetes**](./5-deploy-kubernetes/README.md): Deploy to any Kubernetes cluster using Helm

Or you could not use Aspire for deployment.

### [Chapter 6: Resources](6-resources/README.md)

Additional learning materials:

- [Glossary](6-resources/glossary.md) - Key terms and concepts
- [Resources](6-resources/resources.md) - Official docs, tutorials, and community links


About the Authors
-----------------

### Rob Richardson
[Rob Richardson](http://robrich.org/about/) is a software craftsman building web properties in ASP.NET and Node. He's a frequent speaker at conferences, user groups, and community events, and a diligent teacher and student of high-quality software development.

### Barry Stahl
[Barry Stahl](https://cognitiveinheritance.com/Pages/aboutme.html) is a .NET Software Engineer who has been creating business solutions for enterprise customers for more than 35 years. Barry is also an election-qualified independent for-profit corporate moderator and facilitator.


Contributing
------------

We welcome your contributions, corrections, and feedback! Please give us a pull request or an issue on how we can improve.


License
-------

MIT License, Copyright Richardson & Sons, LLC
