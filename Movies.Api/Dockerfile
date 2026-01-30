# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files for restore (multi-project)
COPY ["Movies.Api/Movies.Api.csproj", "Movies.Api/"]
COPY ["Movies.Application/Movies.Application.csproj", "Movies.Application/"]
COPY ["Movies.Contracts/Movies.Contracts.csproj", "Movies.Contracts/"]

# Restore
RUN dotnet restore "Movies.Api/Movies.Api.csproj"

# Copy rest of the source and publish
COPY . .
WORKDIR /src/Movies.Api
RUN dotnet publish "Movies.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Optional: listen on port 80 inside the container
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Movies.Api.dll"]		