# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore any dependencies
COPY ./src/WeatherForecastApp.Api/WeatherForecastApp.Api.csproj ./WeatherForecastApp.Api/
COPY ./src/WeatherForecastApp.Application/WeatherForecastApp.Application.csproj ./WeatherForecastApp.Application/
COPY ./src/WeatherForecastApp.Domain/WeatherForecastApp.Domain.csproj ./WeatherForecastApp.Domain/
COPY ./src/WeatherForecastApp.Infrastructure/WeatherForecastApp.Infrastructure.csproj ./WeatherForecastApp.Infrastructure/

RUN dotnet restore ./WeatherForecastApp.Api/WeatherForecastApp.Api.csproj

# Copy the remaining files and build the app
COPY ./src/ .
RUN dotnet build ./WeatherForecastApp.Api/WeatherForecastApp.Api.csproj -c Release -o /app/build

# Publish the app to a folder in the container
RUN dotnet publish ./WeatherForecastApp.Api/WeatherForecastApp.Api.csproj -c Release -o /app/publish

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Run the app
ENTRYPOINT ["dotnet", "WeatherForecastApp.Api.dll"]
