#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ECommerce.API.Search/ECommerce.API.Search.csproj", "ECommerce.API.Search/"]
RUN dotnet restore "./ECommerce.API.Search/ECommerce.API.Search.csproj"
COPY . .
WORKDIR "/src/ECommerce.API.Search"
RUN dotnet build "./ECommerce.API.Search.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ECommerce.API.Search.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.API.Search.dll"]