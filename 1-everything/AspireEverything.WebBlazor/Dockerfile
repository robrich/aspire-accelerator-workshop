# FRAGILE: aspire CLI doesn't use this Dockerfile. It uses the .NET SDK to build images instead.

# build "server" image
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

WORKDIR /src

# FRAGILE: ASSUME: there aren't any project dependencies
COPY AspireEverything.WebBlazor.csproj AspireEverything.WebBlazor.csproj

RUN dotnet restore

COPY . .

RUN dotnet build -c Release --no-restore
RUN dotnet test -c Release --no-restore --no-build
RUN dotnet publish -c Release -o /dist --no-restore --no-build


# production "server" image
FROM nginx:alpine

WORKDIR /usr/share/nginx/html
EXPOSE 80

COPY --from=build /dist .
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Nginx already has a CMD
