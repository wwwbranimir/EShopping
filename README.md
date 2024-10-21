# EShopping

Demo for the microservices ecommerce application.

## Overview

This repository contains a microservices-based ecommerce application built with C#, HTML, and Docker.

## Features

- **Microservices Architecture**: Each service is independently deployable and scalable.
- **Docker**: Containerized services for consistent development and deployment.
- **PowerShell**: Scripts for automation and management.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [PowerShell](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell)

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/wwwbranimir/EShopping.git
   cd EShopping
   ```

## Access the Services

- **Nginx Reverse Proxy**: http://localhost:44344
- **Catalog API**: http://localhost:8080
- **Basket API**: http://localhost:8082
- **Discount API**: http://localhost:8084
- **Ordering API**: http://localhost:8086
- **Ocelot API Gateway**: http://localhost:8088
- **IdentityServer**: http://localhost:9011
- **RabbitMQ Management UI**: http://localhost:15672
- **pgAdmin**: http://localhost:5050
- **Portainer**: http://localhost:9090

## Configuration

### Environment Variables

You can configure the services using environment variables defined in the `docker-compose.yml` and `docker-compose.override.yml` files. For example:

- **Catalog API**:
  - `ASPNETCORE_ENVIRONMENT=Development`
  - `ASPNETCORE_URLS=http://+:80`
  - `DatabaseSettings__ConnectionString=mongodb://catalogdb:27017`
  - `DatabaseSettings__DatabaseName=CatalogDb`
  - `DatabaseSettings__CollectionName=Products`
  - `DatabaseSettings__BrandsCollection=Brands`
  - `DatabaseSettings__TypesCollection=Types`
  - `ElasticConfiguration__Uri=http://elasticsearch:9200`

- **Basket API**:
  - `ASPNETCORE_ENVIRONMENT=Development`
  - `ASPNETCORE_URLS=http://+:8082`
  - `CacheSettings__ConnectionString=basketdb:6379`
  - `EventBusSettings__HostAddress=amqp://guest:guest@rabbitmq:5672`
  - `GrpcSettings__DiscountUrl=http://discount.api`

### Volumes

The following volumes are defined to persist data:

- `mongo_data`: MongoDB data
- `portainer_data`: Portainer data
- `postgres_data`: PostgreSQL data
- `pgadmin_data`: pgAdmin data

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
```
