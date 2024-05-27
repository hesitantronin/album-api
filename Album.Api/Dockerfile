# Stage 1: Test
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS test
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Album.Api/*.csproj ./Album.Api/
COPY Album.Api.Tests/*.csproj ./Album.Api.Tests/
RUN dotnet restore

# Copy everything else and build
COPY Album.Api/. ./Album.Api/
COPY Album.Api.Tests/. ./Album.Api.Tests/
WORKDIR /app/Album.Api.Tests
RUN dotnet test

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Metadata
LABEL maintainer="Ronin van Egdom studNr.(1053927)"
LABEL description="This image contains the Album.Api backend"
LABEL version="version1.0"

# Set working dir in container
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Album.Api/*.csproj ./Album.Api/
RUN dotnet restore

# Copy everything else and build
COPY Album.Api/. ./Album.Api/
WORKDIR /app/Album.Api
RUN dotnet publish -c Release -o out

# Stage 3: Serve
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/Album.Api/out .

# Set the environment variable for ASP.NET Core to use port 80
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80

# Entry point for the application
ENTRYPOINT ["dotnet", "Album.Api.dll"]
