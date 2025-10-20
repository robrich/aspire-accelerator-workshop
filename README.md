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

- [Chapter 0: Installation](0-install/README.md): install the prerequisites
- [Chapter 1: Everything Sample](1-everything/README.md): a comprehensive example
- [Chapter 2: Greenfield App](2-greenfield/README.md): get your feet wet with the new project templates
- [Chapter 3: Brownfield App](3-brownfield/README.md): upgrade an existing project to include Aspire
- [Chapter 4: Advanced Aspire](4-aspire-advanced/README.md): play with a lot of Aspire components and integrations
- Chapter 5: Deployment
  - [Docker Compose](./5-deploy-docker/README.md): Prop up other dependencies during development
  - [Azure Container Apps](./5-deploy-aca/README.md): ACA is great when Kubernetes YAML is too confusing
  - [Kubernetes](./5-deploy-kubernetes/README.md): Deploy to any Kubernetes cluster using Helm
  - Or you could use Aspire to scaffold files and manually adjust them.
  - Or don't use Aspire for deployment and deploy with existing patterns.
- [Chapter 6: Resources](6-resources/README.md): More learnings
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

MIT License
