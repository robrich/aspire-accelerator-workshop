# Aspire Roadmap

The following bullet-point list details features that are on the Aspire roadmap, primarily spanning the **2025 through 2026 evolution** planned for the **.NET 10 timeframe**. These features are currently labeled as planned (🛠), in progress (⚠️), or part of the future vision.

[Aspire Roadmap (2025 → 2026)](https://github.com/dotnet/aspire/discussions/10644)

## Key Features

* Local HTTPS Everywhere
* `.localhost` DNS support
* Dev Tunnels
* Debugging in Containers
* Multi-Repo Support
* Polyglot Client Integrations (Python & Typescript)
* Microsoft Agent Framework Integration

## Published Features

### Foundational and AppHost Changes

*   **File-Based AppHost Support:** Preview support for defining a distributed application using a **single `apphost.cs` file**, removing the need for a separate project file (`.csproj`). This aligns with broader efforts in .NET 10 to enable single-file applications.
*   **Single-File AppHost:** Development of a minimal executable version of the AppHost for running Aspire applications without traditional scaffolding.
*   **Deferred Building:** Plans are underway to **remove project references from the AppHost** and defer building until the AppHost runs, which is intended to speed up the local development feedback loop.

### Enhanced Local Developer Experience (DevX)

*   **Improved Networking:** Implementation of **Local HTTPS Everywhere** for consistent TLS support across Aspire resources, external services, and containers.
*   **`.localhost` DNS support** for giving each resource a predictable, friendly name (e.g., `redis.localhost`).
*   **Multi-Repo Support:** Native tooling designed to coordinate Aspire applications that are spread across **multiple repositories**.
*   **Subsets of the App:** The ability to **run only the services necessary** for a task, speeding up feedback loops and reducing startup time.
*   **Debugging in Containers:** Support for **debugging .NET projects while they run inside containers** directly from the IDE.
*   **Built-in Runtime Acquisition:** Aspire will automatically install necessary runtimes (like Node, Python, Java) required to bootstrap various dependencies.
*   **Dev Tunnels:** First-class support for Azure Dev Tunnels, allowing developers to share local web services across the internet.
*   **Resource Life Cycle Event APIs:** Introduction of APIs allowing users to register callbacks for when resources stop, providing better control over cleanup and coordination during application shutdown.

### Dashboard and Observability Improvements

*   **Storage Support:** The ability to **persist dashboard state and telemetry externally** to ensure data survives application restarts or crashes (designed specifically for development and testing workflows).
*   **Advanced Navigation (Cross-Linking):** Implementing seamless navigation between traces, relevant log lines, and metric spikes to simplify diagnostics.
*   **Deployment Flexibility (Deploy Anywhere):** Support for running the dashboard outside Azure environments, including Kubernetes and Docker.
*   **AI-Specific Enhancements:**
    *   **GenAI Visualizer:** A display that collates, summarizes, and visualizes calls to large language models (LLMs) within an app (previewed in Aspire 9.5).
    *   A **Conversational View** for LLM interactions.
    *   **Token Usage Visualization**.
    *   Native support for **LLM-Specific Metrics** (such as model name and function call traces).

### Tooling, Polyglot Support, and AI Workflows

*   **Unified CLI:** A comprehensive **Aspire CLI** with unified commands such as `init`, `update` (previewed in 9.5), and `run`.
*   **Installers:** Standard installation support through package managers like **Homebrew and Winget**.
*   **VS Code Extension:** A planned extension to run, debug, and orchestrate Aspire applications across **C#, JavaScript, and Python**.
*   **Polyglot Client Integrations:** Providing **Consistent Client Integrations** (typed connection strings, configuration, and telemetry) that work uniformly across languages, starting with packages for `pip` and `npm`.
*   **Cross-Language AppHost (Experimental):** Exploring the use of **WASM + WIT** to support multiple runtimes within a single process.
*   **AI Integrations:**
    *   Native support for building agent-based apps via **Azure AI Foundry Agent Support**.
    *   The ability to configure **OpenAI models** directly from the AppHost (with an `AddOpenAI` integration previewed in 9.5).

### Deployment and Hosting

*   **Formalized Publish/Deploy Commands:** Utilization of **`aspire publish`** to generate parameterized deployment artifacts (Compose files, manifests) and **`aspire deploy`** to resolve parameters and execute the deployment.
*   **Kubernetes Deployment:** The goal is for the built-in Kubernetes integration (currently "In Progress") to replace community solutions like Aspirate.
    *   `aspire publish` will produce **Kustomize/Helm charts**.
    *   `aspire deploy` will apply them to a cluster.
*   **Environment Support:** The ability to define discrete **environments** (e.g., dev, stage, prod) which include environment-scoped configuration, secrets, and outputs.
*   **CI/CD Integration:** Planned functionality to **generate CI/CD pipelines** (including scaffolding for secret handling) for GitHub Actions, Azure DevOps, and GitLab.
*   **New Azure Hosting Integrations:** Planned integrations for advanced Azure services including **VNet Modeling**, **Event Grid**, **Azure Front Door**, **Azure API Management (APIM)**, **Kusto (ADX)**, and **Azure Redis Enterprise**.
*   **Authentication:** Planned support for popular **Auth Providers** like Entra ID, Keycloak, and Easy Auth.
*   **Infrastructure as Code (Future State):** Plans exist for Infrastructure as Code using Aspire.
*   **Preview Deploy Capabilities:** Deployment support is currently in **Preview** and may change for integrations like `Aspire.Hosting.Azure.AppContainers` (Azure Container Apps) and `Aspire.Hosting.Azure.AppService` (Azure App Service).

### Microsoft Agent Framework (Public Preview) and Responsible AI

Features related to Microsoft Agent Framework, which integrates with Azure AI Foundry Agent Support, include:

*   The **Microsoft Agent Framework** itself, now in **public preview**, unifying AutoGen and Semantic Kernel.
*   **Multi-agent workflows in Foundry Agent Service**, currently in **private preview**, enabling the orchestration of sophisticated, multi-step business processes with persistent state, error handling, and visual debugging.
*   Enhancements to **multi-agent observability** via contributions to **OpenTelemetry** for standardizing tracing and telemetry in agentic systems.
*   Responsible AI capabilities entering **public preview** soon:
    *   Task adherence.
    *   Prompt shields with spotlighting.
    *   **PII detection** (Personally Identifiable Information detection).