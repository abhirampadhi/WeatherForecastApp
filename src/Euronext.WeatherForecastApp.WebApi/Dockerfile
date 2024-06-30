#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Euronext.WeatherForecastApp.WebApi/Euronext.WeatherForecastApp.WebApi.csproj", "src/Euronext.WeatherForecastApp.WebApi/"]
COPY ["src/Euronext.WeatherForecastApp.Application/Euronext.WeatherForecastApp.Application.csproj", "src/Euronext.WeatherForecastApp.Application/"]
COPY ["src/Euronext.WeatherForecastApp.Common/Euronext.WeatherForecastApp.Common.csproj", "src/Euronext.WeatherForecastApp.Common/"]
COPY ["src/Euronext.WeatherForecastApp.Domain/Euronext.WeatherForecastApp.Domain.csproj", "src/Euronext.WeatherForecastApp.Domain/"]
COPY ["src/Euronext.WeatherForecastApp.Infrastructure/Euronext.WeatherForecastApp.Infrastructure.csproj", "src/Euronext.WeatherForecastApp.Infrastructure/"]
RUN dotnet restore "./src/Euronext.WeatherForecastApp.WebApi/Euronext.WeatherForecastApp.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Euronext.WeatherForecastApp.WebApi"
RUN dotnet build "./Euronext.WeatherForecastApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Euronext.WeatherForecastApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Euronext.WeatherForecastApp.WebApi.dll"]