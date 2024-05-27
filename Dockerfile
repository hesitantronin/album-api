# Stage 1: Test
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS test
WORKDIR /app

# Copy everything and restore as distinct layers
COPY . .
RUN dotnet restore

# Run unit tests
WORKDIR /app/Album.Api.Tests
RUN dotnet test

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and build
COPY . .
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


# Metadata
LABEL maintainer="Ronin van Egdom studNr.(1053927)"
LABEL description="This image contains the Album.Api backend"
LABEL version="version1.0"
