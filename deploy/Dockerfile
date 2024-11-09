FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source

COPY ../CoreGoDelivery.sln .
COPY ../src/CoreGoDelivery.Api/CoreGoDelivery.Api.csproj src/CoreGoDelivery.Api/

RUN dotnet restore src/CoreGoDelivery.Api/CoreGoDelivery.Api.csproj

COPY ../src/ src/

RUN dotnet publish src/CoreGoDelivery.Api/CoreGoDelivery.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5273

ENTRYPOINT ["dotnet", "CoreGoDelivery.Api.dll"]
