Install Instructions
====================

You'll need a few things installed to use the content in this workshop:

1. Install an IDE:

   Pick one:

   - [Visual Studio](https://visualstudio.microsoft.com/downloads/) with the ASP.NET Core workload
   - [VS Code](https://code.visualstudio.com/download)
   - your favorite editor that can edit both .NET and Node.js projects.

2. Install the [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet).

3. Install and upgrade the Azure Functions Runtime

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

4. Upgrade the Azure Functions new project templates:

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

5. Install or upgrade the Aspire CLI

   Open a terminal in any directory and run:

   ```sh
   dotnet tool install --global aspire.cli
   ```

7. Install [Node.js](https://nodejs.org/en/download) through the download on this site or through a Node Version Manager like [nvm](!!!!!!!!!!!!!!!)

8. Install a container runtime:

   Pick one:

   - [Docker Desktop](https://www.docker.com/products/docker-desktop/)
   - [Podman Desktop](https://podman-desktop.io/downloads)
