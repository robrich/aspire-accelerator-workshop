# FRAGILE: aspire CLI doesn't use this Dockerfile. It uses the .NET SDK to build images instead.

# build "server" image
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

WORKDIR /src

COPY AspireEverything.slnx .
COPY AspireEverything.AppHost/AspireEverything.AppHost.csproj AspireEverything.AppHost/AspireEverything.AppHost.csproj
COPY AspireEverything.FrameworkApi/AspireEverything.FrameworkApi.csproj AspireEverything.FrameworkApi/AspireEverything.FrameworkApi.csproj
COPY AspireEverything.FrameworkApi.Tests/AspireEverything.FrameworkApi.Tests.csproj AspireEverything.FrameworkApi.Tests/AspireEverything.FrameworkApi.Tests.csproj
COPY AspireEverything.ServiceDefaults/AspireEverything.ServiceDefaults.csproj AspireEverything.ServiceDefaults/AspireEverything.ServiceDefaults.csproj
COPY AspireEverything.VoteData/AspireEverything.VoteData.csproj AspireEverything.VoteData/AspireEverything.VoteData.csproj
COPY AspireEverything.VoteGet/AspireEverything.VoteGet.csproj AspireEverything.VoteGet/AspireEverything.VoteGet.csproj
COPY AspireEverything.VoteScore/AspireEverything.VoteScore.csproj AspireEverything.VoteScore/AspireEverything.VoteScore.csproj
COPY AspireEverything.WebBlazor/AspireEverything.WebBlazor.csproj AspireEverything.WebBlazor/AspireEverything.WebBlazor.csproj

RUN dotnet restore

COPY . .

RUN dotnet build -c Release --no-restore
RUN dotnet test -c Release --logger:"junit;LogFilePath=../results/unit.xml" --no-restore --no-build --filter "Type!=Integration"
RUN dotnet publish AspireEverything.FrameworkApi/AspireEverything.FrameworkApi.csproj -c Release -o /dist/api --no-restore --no-build
RUN dotnet publish AspireEverything.VoteGet/AspireEverything.VoteGet.csproj -c Release -o /dist/func-voteget --no-restore --no-build
RUN dotnet publish AspireEverything.VoteScore/AspireEverything.VoteScore.csproj -c Release -o /dist/func-votescore --no-restore --no-build
RUN dotnet publish AspireEverything.WebBlazor/AspireEverything.WebBlazor.csproj -c Release -o /dist/blazor --no-restore --no-build


# https://kevsoft.net/2021/08/09/exporting-unit-test-results-from-a-multi-stage-docker-build.html
# FRAGILE: only exports test results if they pass
FROM scratch AS export-test-results
COPY --from=build /src/results/** .


# test "server" image
FROM build AS test

# TODO set env vars as you start tests
ENV ConnectionStrings__cache=""
ENV ConnectionStrings__voting=""

CMD ["dotnet", "test", "-c", "Release", "--no-restore", "--logger:junit;LogFilePath=../results/integration.xml"]


# production runtime "server" image for VoteGet function
# TODO: is there an alpine version?
FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated9.0 AS func-voteget
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

WORKDIR /home/site/wwwroot
EXPOSE 80

COPY --from=build ["/dist/func-voteget", "/home/site/wwwroot"]


# production runtime "server" image for VoteScore function
# TODO: is there an alpine version?
FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated9.0 AS func-votescore
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

WORKDIR /home/site/wwwroot
EXPOSE 80

COPY --from=build ["/dist/func-votescore", "/home/site/wwwroot"]


# production runtime "server" image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS frameworkapi

# trust x-forwarded-for headers in linux
# https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?#forward-the-scheme-for-linux-and-non-iis-reverse-proxies
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080

# Env vars injected as the container starts:
ENV ConnectionStrings__cache=""
# ^ Redis
ENV ConnectionStrings__voting=""
# ^ Postgres

USER app

WORKDIR /app

COPY --from=build --chown=app /dist/api .
CMD ["dotnet", "AspireEverything.FrameworkApi.dll"]


# production runtime "server" image for Blazor WebAssembly
FROM nginx:alpine AS blazor
WORKDIR /usr/share/nginx/html
EXPOSE 80
COPY --from=build /dist/blazor .
COPY AspireEverything.WebBlazor/nginx.conf /etc/nginx/nginx.conf
# Nginx has its own CMD
