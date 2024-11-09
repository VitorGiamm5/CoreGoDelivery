Go Core Delivery

Especificações:
- Dotnet Core 8.0
- Entity Framework
- DocsBRValidator (Extenção Validador de documentos)
- Postgres
- RabbitMQ
- Docker
- Docker compose
- Polly
- xUnit
- IO de arquivos em HD

#How to run
1. Abra o terminal, preferencialmente na raiz C:/ (opcional)
$ mkdir Projetos

2. No terminal clonar:
$ git clone https://github.com/VitorGiamm5/CoreGoDelivery.git

3. Entrar na raíz do projeto:
$ cd CoreGoDelivery

4. Executar o docker compose:
$ docker-compose -f deploy/docker-compose.yml down
$ docker-compose -f deploy/docker-compose.yml up --build

Observação:
- Depois que o docker baixar todas as imagens ele auto inicia os serviços
- A aplicação .Net possui a injeção do Polly, isso maximiza que a conexão entre os serviços sejam realizadas dentro do docker

5. Verificar se os serviços estão online (postgre, rabbitmq e deploy-coregodelivery)
$ docker ps -a

7. Executar as migrations (esteja com seu terminal na raíz do projeto "c:/Projetos/CoreGoDelivery")
$ dotnet ef database update -s src\CoreGoDelivery.Api -p src\CoreGoDelivery.Infrastructure

8. Para conectar o Banco recomenda-se usar o DBeaver, para facilitar a importação de dados que serão necessários!

Host: localhost
Port: 5432
Bando de dados: dbgodelivery
Nome de usuário: randandan
Senha: randandan_XLR

7. Depois de conectado e com as tables criadas, necessário injetar dados diretamente no banco, seguindo o seguinte passo a passo:
Na raiz do projeto abra a poasta "Assets" e depois "SQL-Importar-Dados", veja que para cada tabela há um arquivo .csv correlato para importar.
Para importar os dados, você deve abrrir o DBeaver, ir até a collection, ver as tables e com o botão direito ir em "importar dados" .csv, selecione o arquivo com o nome correlato com a tabela e importar, recomenda-se fazer isso em todas as tabelas, no entanto, as principais e cruciais são: tb_modelMotorcycle e tb_RentalPlan

Atenção:
É obrigatório importar os csv => tb_modelMotorcycle e tb_RentalPlan
ATENÇÃO CASO NÃO IMPORTE A APLICAÇÃO NÃO FUNCIONARÁ

8. Para facilitar o consumo da Api, está disponível na pasta Asset > postmanCollection, o arquivo de colection para importar no postman

9. Pronto para usar!

===
Notas e dicas de uso:

Para os end-points que necessitam de imagem base64, elas estão disponíveis na pasta "Assets" e então na pasta "ImageCNH", nela contém uma imagem .png e uma .bmp e de brinde, arquivos de texto com as imagens já em base64!

Caso modifique alguma entidade, esse é o comando para criar a migration

Gerar migration, considere abrir o Powershell na pasta raiz do projeto: 
$ dotnet ef migrations add InicialBase -s src\CoreGoDelivery.Api -p src\CoreGoDelivery.Infrastructure

Atualizar o banco:
$ dotnet ef database update -s .\CoreGoDelivery.Api -p .\CoreGoDelivery.Infrastructure

Referências:

- Validador de documentos
https://www.nuget.org/packages/DocsBRValidator

- Página administrativa do RabbitMQ
http://localhost:15672/#/