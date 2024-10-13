#Stage 1
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /build
COPY "CoreGoDelivery.API/CoreGoDelivery.Api.csproj" .
COPY "CoreGoDelivery.Infrastructure/CoreGoDelivery.Infrastructure.csproj" .
COPY "CoreGoDelivery.Application/CoreGoDelivery.Application.csproj" .
COPY "CoreGoDelivery.Domain/CoreGoDelivery.Domain.csproj" .

RUN dotnet restore "CoreGoDelivery.Api.csproj"
RUN dotnet publish "CoreGoDelivery.Api.csproj" -c Release -o "/app"

#Stage 2
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final

WORKDIR /app

EXPOSE 5000
ENTRYPOINT ["dotnet", "CoreGoDelivery.Api.dll"]
