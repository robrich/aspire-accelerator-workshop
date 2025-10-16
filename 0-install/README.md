Install Instructions
====================


Table of Contents
-----------------

1. [Install an IDE](#1-install-an-ide)
2. [Install the .NET 9 SDK](#2-install-the-net-9-sdk)
3. [Install or upgrade the Aspire project templates](#3-install-or-upgrade-the-aspire-project-templates)
4. [Install or upgrade the Aspire CLI](#4-install-or-upgrade-the-aspire-cli)
5. [Install or upgrade the Azure Functions Runtime](#5-install-or-upgrade-the-azure-functions-runtime)
6. [Upgrade the Azure Functions new project templates](#6-upgrade-the-azure-functions-new-project-templates)
7. [Install the latest Node.js](#7-install-the-latest-nodejs)
8. [Install a container runtime](#8-install-a-container-runtime)


Install steps
-------------

You'll need a few things installed to use the content in this workshop:

1. Install an IDE:

   Pick one:

   - [Visual Studio](https://visualstudio.microsoft.com/downloads/) with the ASP.NET Core workload
   - [VS Code](https://code.visualstudio.com/download)
   - your favorite editor that can edit both .NET and Node.js projects.

   *Why?* An IDE is required to edit, build, and debug the .NET and Node.js projects in this workshop.

2. Install the [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet).

   *Why?* The .NET 9 SDK is required to build and run the backend services and APIs in this workshop.

3. Install or upgrade the Aspire project templates:

   Open a terminal in any directory and run:

   ```sh
   dotnet new install Aspire.ProjectTemplates
   dotnet new update
   ```

   *Why?* Aspire project templates provide the scaffolding and structure for Aspire-based .NET projects used in this workshop.

4. Install or upgrade the Aspire CLI

   Open a terminal in any directory and run:

   ```sh
   dotnet tool install --global aspire.cli
   dotnet tool update --global aspire.cli
   ```

   *Why?* The Aspire CLI is used to manage, run, and deploy Aspire projects from the command line.

5. Install or upgrade the Azure Functions Runtime

   a. Install the latest [Azure Functions Runtime](https://github.com/Azure/azure-functions-core-tools#installing) using your favorite method:

      - Windows MSI: https://go.microsoft.com/fwlink/?linkid=2174087
      - NPM: `npm i -g azure-functions-core-tools@4`
      - Chocolatey: `choco install azure-functions-core-tools`
      - WinGet: `winget install Microsoft.Azure.FunctionsCoreTools`
      - MacOS: `brew tap azure/functions; brew install azure-functions-core-tools@4`
      - Linux: See https://github.com/Azure/azure-functions-core-tools#linux

   OR

   b. Upgrade the Azure Functions Runtime:

      - Windows MSI: https://go.microsoft.com/fwlink/?linkid=2174087
      - NPM `npm install -g azure-functions-core-tools@4`
      - Chocolatey: `choco upgrade azure-functions-core-tools`
      - WinGet: `winget upgrade Microsoft.Azure.FunctionsCoreTools`
      - MacOS: `brew upgrade azure-functions-core-tools`
      - Linux: See https://github.com/Azure/azure-functions-core-tools#linux

   *Why?* The Azure Functions Runtime is required to run and debug Azure Functions locally, which are used in this workshop.

6. Upgrade the Azure Functions new project templates:

   In Visual Studio:

   - Open Visual Studio
   - Go to Tools -> Options
   - On the left, in the Projects & Solutions section, choose Azure Functions
   - On the right click "Check for Updates"
   - If it appears, click the "Download and Apply" button

   OR

   In VS Code:

   - Open the command palette: cntrl + shift + p
   - Run `Azure Functions: Install or Update Core Tools`

   *Why?* Up-to-date project templates ensure you have the latest features and compatibility for creating new Azure Functions projects.

7. Install the latest Node.js:

   Download and install [Node.js](https://nodejs.org/en/download)

   OR

   Use a Node Version Manager like [nvm](https://github.com/nvm-sh/nvm) or [nvm for windows](https://github.com/coreybutler/nvm-windows)

   *Why?* Node.js is required to build and run the React and Vue frontend projects included in this workshop.

8. Install a container runtime:

   Pick one:

   - [Docker Desktop](https://www.docker.com/products/docker-desktop/)
   - [Podman Desktop](https://podman-desktop.io/downloads)

   *Why?* A container runtime is needed to build, run, and test containerized applications and services locally.

9. Clone this repository onto your machine.

   *Why?* You'll modify the code in this repository as part of this course.
