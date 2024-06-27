# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore any dependencies
COPY .src/Euronext.WeatherForecastApp.WebApi\Euronext.WeatherForecastApp.WebApi.csproj ./Euronext.WeatherForecastApp.WebApi/
COPY .src/Euronext.WeatherForecastApp.Application\Euronext.WeatherForecastApp.Application.csproj ./Euronext.WeatherForecastApp.Application
COPY .src/Euronext.WeatherForecastApp.Application\Euronext.WeatherForecastApp.Common.csproj ./Euronext.WeatherForecastApp.Common
COPY .src/Euronext.WeatherForecastApp.Application\Euronext.WeatherForecastApp.Domain.csproj ./Euronext.WeatherForecastApp.Domain
COPY .src/Euronext.WeatherForecastApp.Application\Euronext.WeatherForecastApp.Infrastructure.csproj ./Euronext.WeatherForecastApp.Infrastructure

RUN dotnet restore ./Euronext.WeatherForecastApp.WebApi/Euronext.WeatherForecastApp.WebApi.csproj

# Copy the remaining files and build the app
COPY ./src/ .
RUN dotnet build ./Euronext.WeatherForecastApp.WebApi/Euronext.WeatherForecastApp.WebApi.csproj -c Release -o /app/build

# Publish the app to a folder in the container
RUN dotnet publish ./Euronext.WeatherForecastApp.WebApi/Euronext.WeatherForecastApp.WebApi.csproj -c Release -o /app/publish

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Run the app
ENTRYPOINT ["dotnet", "Euronext.WeatherForecas.Api.dll"]
