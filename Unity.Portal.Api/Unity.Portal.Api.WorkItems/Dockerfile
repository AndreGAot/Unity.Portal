FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5090

ENV ASPNETCORE_URLS=http://+:5090

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Unity.Portal.Api.WorkItems.csproj", "Unity.Portal.Api.WorkItems/"]
RUN dotnet restore "Unity.Portal.Api.WorkItems/Unity.Portal.Api.WorkItems.csproj"

WORKDIR "/src/Unity.Portal.Api.WorkItems"
COPY . .
RUN dotnet build "Unity.Portal.Api.WorkItems.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Unity.Portal.Api.WorkItems.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unity.Portal.Api.WorkItems.dll"]
