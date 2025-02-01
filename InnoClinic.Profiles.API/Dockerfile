# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["InnoClinic.Profiles.API/InnoClinic.Profiles.API.csproj", "InnoClinic.Profiles.API/"]
COPY ["InnoClinic.Profiles.Application/InnoClinic.Profiles.Application.csproj", "InnoClinic.Profiles.Application/"]
COPY ["InnoClinic.Profiles.Core/InnoClinic.Profiles.Core.csproj", "InnoClinic.Profiles.Core/"]
COPY ["InnoClinic.Profiles.DataAccess/InnoClinic.Profiles.DataAccess.csproj", "InnoClinic.Profiles.DataAccess/"]
COPY ["InnoClinic.Profiles.Infrastructure/InnoClinic.Profiles.Infrastructure.csproj", "InnoClinic.Profiles.Infrastructure/"]

RUN dotnet restore "InnoClinic.Profiles.API/InnoClinic.Profiles.API.csproj"

COPY . .

WORKDIR "/src/InnoClinic.Profiles.API"
RUN dotnet build "InnoClinic.Profiles.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "InnoClinic.Profiles.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnoClinic.Profiles.API.dll"]