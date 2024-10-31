# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia a solução e arquivos de projeto para restaurar as dependências
COPY ../CoreGoDelivery.sln .
COPY ../src/CoreGoDelivery.API/CoreGoDelivery.Api.csproj src/CoreGoDelivery.API/

# Restaura as dependências
RUN dotnet restore src/CoreGoDelivery.API/CoreGoDelivery.Api.csproj

# Copia o código fonte e o appsettings.json para o container
COPY ../src/ src/
COPY ../src/CoreGoDelivery.API/appsettings.json src/CoreGoDelivery.API/

# Compila a aplicação
RUN dotnet publish src/CoreGoDelivery.API/CoreGoDelivery.Api.csproj -c Release -o /app/publish

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados para o container
COPY --from=build /app/publish .

# Exponha a porta 5273 para a aplicação
EXPOSE 5273

# Usar o script wait-for-it como entrypoint para esperar pelos serviços antes de iniciar a aplicação
ENTRYPOINT ["dotnet", "CoreGoDelivery.Api.dll"]
