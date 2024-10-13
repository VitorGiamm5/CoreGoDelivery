#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/CoreGoDelivery.API/CoreGoDelivery.Api.csproj", "src/Apps/CoreGoDelivery.Api/"]
COPY ["src/CoreGoDelivery.Infrastructure/CoreGoDelivery.Infrastructure.csproj", "src/CoreGoDelivery.Api.Infrastructure/"]
COPY ["src/CoreGoDelivery.Application/CoreGoDelivery.Application.csproj", "src/CoreGoDelivery.Api.Application/"]
COPY ["src/CoreGoDelivery.Domain/CoreGoDelivery.Domain.csproj", "src/CoreGoDelivery.Api.Domain/"]
RUN dotnet restore "src/Apps/CoreGoDelivery.Api/ifrs9_api.Api.csproj"
COPY . .
WORKDIR "/src/src/Apps/CoreGoDelivery.Api"
RUN dotnet build "CoreGoDelivery.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreGoDelivery.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreGoDelivery.Api.dll"]
