#Stage 1
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build
WORKDIR /build
COPY /src .

RUN dotnet restore "CoreGoDelivery.API/CoreGoDelivery.Api.csproj"
RUN dotnet publish "CoreGoDelivery.API/CoreGoDelivery.Api.csproj" -c Release -o "/app" --no-restore

#Stage 2
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final

WORKDIR /app

COPY --from=build /app .

EXPOSE 5000
ENTRYPOINT ["dotnet", "CoreGoDelivery.Api.dll"]
