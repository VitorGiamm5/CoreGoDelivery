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
Na raíz do projeto
$ docker-compose -f deploy/docker-compose.yml down
$ docker-compose -f deploy/docker-compose.yml up --build

Necessário startar as imagens!
o docker compose contém as imagens do Postgres e do RabbitMQ

Depois que as imagens estiverem criadas e executando, necessário rodar a migration
$ cd src
$ dotnet ef database update -s .\CoreGoDelivery.API -p .\CoreGoDelivery.Infrastructure

Para conectar o Banco recomenda-se usar o DBeaver, para facilitar a importação de dados que serão necessários

Host: localhost
Port: 5432
Bando de dados: dbgodelivery
Nome de usuário: randandan
Senha: randandan_XLR

Depois de conectado e com as tables criadas, necessário injetar dados diretamente no banco
Na raiz do projeto abra a poasta "Assets" e depois "SQL-Importar-Dados", veja que para cada tabela há um arquivo .csv correlato para importar
Para importar os dados, você deve abrrir o DBeaver, ir até a collection, ver as tables e com o botão direito ir em importar dados, selecione o arquivo com o nome correlato com a tabela

ATENÇÃO CASO NÃO IMPORTE OS DADOS A APLICAÇÃO NÃO FUNCIONARÁ

Para chamar os recursos da API, está disponível na raíZ do projeto Na pasta "Assets" depois em "postmanCollection" a collection para importar no postman

Para os end-points que necessitam de imagem, elas estão disponíveis na pasta "Assets" e então na pasta "ImageCNH", nela contém uma imagem .png e uma .bmp e de brinde um arquivo de texto com as imagens já em base64!

===
Notas:

Caso modifique alguma entidade, esse é o comando para criar a migration

Gerar migration, considere abrir o Powershell na pasta src do projeto

dotnet ef migrations add InicialBase -s .\CoreGoDelivery.API -p .\CoreGoDelivery.Infrastructure

Atualizar o banco

dotnet ef database update -s .\CoreGoDelivery.API -p .\CoreGoDelivery.Infrastructure

Referências:

- Validador de documentos
https://www.nuget.org/packages/DocsBRValidator

- Página administrativa do RabbitMQ
http://localhost:15672/#/