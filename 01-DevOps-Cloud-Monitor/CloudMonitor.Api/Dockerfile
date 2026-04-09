# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["CloudMonitor.Api.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "CloudMonitor.Api.dll"]	