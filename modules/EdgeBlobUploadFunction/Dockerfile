# Base
FROM mcr.microsoft.com/azure-functions/dotnet:3.0 AS base

RUN apt-get update && \
    apt-get install -y --no-install-recommends \
      unzip \
      procps \
      && \
    rm -rf /var/lib/apt/lists/*

# Build Env
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
WORKDIR /app

COPY *.csproj /app
RUN dotnet restore

COPY . /app
RUN mkdir -p /home/site/wwwroot
RUN dotnet publish *.csproj --output /home/site/wwwroot

# Release
FROM base
EXPOSE 80
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=false

COPY --from=build-env ["/home/site/wwwroot", "/home/site/wwwroot"]