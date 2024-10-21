Go Core Delivery

Especificações:
Dotnet core 8.0
- Entity Framework
- Validador de documentos
Postgres
RabigMQ
Docker compose

#How to run
Preferencialmente clonar na pasta C:/Projetos

No terminal clonar:
$ git clone https://github.com/VitorGiamm5/CoreGoDelivery.git

$ cd CoreGoDelivery
$ dotnet restore

Executar o docker compose:
$ docker compose -f "deploy\docker-compose.yml" up -d --build

Deepois que as imagens estiverem criadas e executando, necessário rodar a migration
$ cd src
$ dotnet ef database update -s .\CoreGoDelivery.API -p .\CoreGoDelivery.Infrastructure

Para conectar o DBeaver (por exemplo), tanvés seja necessário instalar o recurso no DBeaver

Host: localhost
Port: 5432
Bando de dados: dbgodelivery
Nome de usuário: randandan
Senha: randandan_XLR

Depois de conectado e com as tables criadas, necessário injetar dados diretamente no banco
Na raiz do projeto a pasta "sqlScriptImportar" possui dois arquivos .csv
$ cd ..
$ cd sqlScriptImportar
Cada arquivo é nomeado com o nome da table para ser importada: _tb_modelMotocycle_importar.csv e _tb_rentalPlan_importar.csv
Clicando com o botão direito no nome da table, terá a opção de importar, basta selecionar o arquivo correspondente à table

Para chamar os recursos da API, está disponível na raís do projeto a pasta "postmanCollection"


Dicas para o desenvolcedor para as migrations:

Gerar migration, considere abrir o Powershell na pasta src do projeto

dotnet ef migrations add InicialBase -s .\CoreGoDelivery.API -p .\CoreGoDelivery.Infrastructure

Atualizar o banco

dotnet ef database update -s .\CoreGoDelivery.API -p .\CoreGoDelivery.Infrastructure

Referências:

- Validador de documentos
https://www.nuget.org/packages/DocsBRValidator

- Página administrativa do RabbitMQ
http://localhost:15672/#/