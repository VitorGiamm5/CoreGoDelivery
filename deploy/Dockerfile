# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia a solução e o projeto principal
COPY ../CoreGoDelivery.sln .
COPY ../src/CoreGoDelivery.API/CoreGoDelivery.Api.csproj src/CoreGoDelivery.API/

# Restaura as dependências
RUN dotnet restore src/CoreGoDelivery.API/CoreGoDelivery.Api.csproj

# Copia todo o código-fonte
COPY ../src/ src/

# Compila a aplicação
RUN dotnet publish src/CoreGoDelivery.API/CoreGoDelivery.Api.csproj -c Release -o /app/publish

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponha a porta para a aplicação
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "CoreGoDelivery.Api.dll"]
