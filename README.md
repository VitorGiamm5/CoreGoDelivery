# Go Core Delivery
Main application for managing motorcycles, rentals and drivers.

## Specifications:
- Dotnet Core 8 LTS
- Entity Framework (ORM)
- CQRS
- RabbitMQ (Queue)
- PostgreSQL (Relational Database)
- DocsBRValidator (brazilian documents validator)
- MinIO (File server)
- Docker
- Docker compose
- Polly (Circuit break and as a connection factory)
- xUnit with Moq (Unit test and mock data)

## Tech Features
- CRUD Motorcycle
- CRUD Rentals
- CRUD Divers
- Motorcycle models list
- License Driver Image: Upload, storage, Get
- Send a mensage to RabbitMQ when a new motorcycle is added and save the mensage in other database

## Business Features
- Calculate the motorcycle **Rental value**
- **Plans** of retal
- **Motorcycle** and **driver** must be **Unic**
- **Restrict** new rentals for motorcycles **currently in use**
- Validate license driver data

# How to run

### 1. Clone the project
``git clone https://github.com/VitorGiamm5/CoreGoDelivery.git``

### 2. Create local infrasctructure:
This step will download docker images, and put it into run
``./local-infrastructure/start.sh``

### 3. Connections
- PosgreSQL (Database)
``Host: localhost``
``Port: 9000``
``Database: dbgodelivery``
``User name: randandan``
``Key access: randandan_XLR``

- RabbitMQ (Queue)
``Host: localhost``
``Port: 9002``
``Queue: motorcycle_notification_queue``
``User name: guest``
``Key access: guest``

- Mini IO (Local File Server)
``Host: localhost``
``Port: 9004``
``Bucket: motorcycle_notification_queue``
``User name: guest``
``Key access: guest``

### 4. Check if dotnet-ef is installed
``dotnet tool install --global dotnet-ef``

===

### Usage data migration guide

``dotnet ef migrations add InicialBase -s .\src\CoreGoDelivery.Api -p .\src\CoreGoDelivery.Infrastructure``


``dotnet ef database update -s .\src\CoreGoDelivery.Api -p .\src\CoreGoDelivery.Infrastructure``

## References:

- Brazilian documents validator
https://www.nuget.org/packages/DocsBRValidator

- RabbitMQ Administrative page
http://localhost:9002/#/

- Mini IO Administrative page
http://localhost:9004

Service name    Internal port	external port	
PostgreSQL	    5432	        9000	       
RabbitMQ (AMQP)	5672	        9001	       
RabbitMQ (UI)	15672	        9002	       
MinIO (API)	    9000	        9003	       
MinIO (Console)	9001	        9004	       
CoreGoDelivery	80	            9005	       
