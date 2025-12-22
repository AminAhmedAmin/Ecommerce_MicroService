# Ordering Microservice

## Overview
The Ordering microservice is responsible for managing customer orders in the e-commerce system. It follows Clean Architecture principles with CQRS pattern and uses SQL Server as the database.

## Architecture

### Project Structure
```
Ordering/
├── Ordering.Core/              # Domain layer
│   ├── Entities/              # Domain entities
│   │   ├── BaseEntity.cs
│   │   └── Order.cs
│   └── Repositories/          # Repository interfaces
│       ├── IAsyncRepository.cs
│       └── IOrderRepository.cs
│
├── Ordering.Application/       # Application layer
│   ├── Commands/              # CQRS Commands
│   │   ├── CheckoutOrderCommand.cs
│   │   ├── UpdateOrderCommand.cs
│   │   └── DeleteOrderCommand.cs
│   ├── Queries/               # CQRS Queries
│   │   └── GetOrderListQuery.cs
│   ├── Handlers/              # MediatR Handlers
│   │   ├── CheckoutOrderHandler.cs
│   │   ├── UpdateOrderHandler.cs
│   │   ├── DeleteOrderHandler.cs
│   │   └── GetOrderListHandler.cs
│   ├── Responses/             # DTOs
│   │   └── OrderResponse.cs
│   └── Mapper/                # AutoMapper profiles
│       └── OrderMappingProfile.cs
│
├── Ordering.Infrastructure/    # Infrastructure layer
│   ├── Data/                  # Database context
│   │   └── OrderContext.cs
│   └── Repositories/          # Repository implementations
│       ├── RepositoryBase.cs
│       └── OrderRepository.cs
│
└── Ordering.API/              # Presentation layer
    ├── Controllers/
    │   └── OrderController.cs
    ├── Program.cs
    ├── appsettings.json
    └── Dockerfile
```

## Technologies Used
- **.NET 10.0** - Framework
- **Entity Framework Core 10.0** - ORM
- **SQL Server 2022** - Database
- **MediatR** - CQRS implementation
- **AutoMapper** - Object mapping
- **Swagger** - API documentation

## Design Patterns
1. **Clean Architecture** - Separation of concerns with distinct layers
2. **CQRS** - Command Query Responsibility Segregation
3. **Repository Pattern** - Generic repository with async operations
4. **Dependency Injection** - Built-in .NET DI container

## API Endpoints

### Get Orders by Username
```http
GET /api/v1/Order/{userName}
```
Returns all orders for a specific user.

### Checkout Order
```http
POST /api/v1/Order
Content-Type: application/json

{
  "userName": "string",
  "totalPrice": 0,
  "firstName": "string",
  "lastName": "string",
  "emailAddress": "string",
  "addressLine": "string",
  "country": "string",
  "state": "string",
  "zipCode": "string",
  "cardName": "string",
  "cardNumber": "string",
  "expiration": "string",
  "cvv": "string",
  "paymentMethod": 0
}
```

### Update Order
```http
PUT /api/v1/Order
Content-Type: application/json

{
  "id": 0,
  "userName": "string",
  "totalPrice": 0,
  ...
}
```

### Delete Order
```http
DELETE /api/v1/Order/{id}
```

## Database Configuration

### Connection String
```json
{
  "ConnectionStrings": {
    "OrderingConnectionString": "Server=localhost,1433;Database=OrderDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"
  }
}
```

### Running Migrations
```bash
# Navigate to the Infrastructure project
cd Services/Ordering/Ordering.Infrastructure

# Add migration
dotnet ef migrations add InitialCreate --startup-project ../Ordering.API

# Update database
dotnet ef database update --startup-project ../Ordering.API
```

## Docker Configuration

The service is configured in `docker-compose.yml`:
- **Ordering API**: Port 8003
- **SQL Server**: Port 1433

### Run with Docker Compose
```bash
docker-compose up -d orderdb
docker-compose up -d ordering.api
```

## Development

### Build the project
```bash
dotnet build
```

### Run locally
```bash
cd Services/Ordering/Ordering.API
dotnet run
```

The API will be available at `http://localhost:5000` (or the port specified in launchSettings.json)

### Access Swagger UI
Navigate to `http://localhost:8003/swagger` when running in Docker, or `http://localhost:5000/swagger` when running locally.

## Generic Repository Features

The generic repository (`IAsyncRepository<T>`) provides:
- `GetAllAsync()` - Get all entities
- `GetAsync(predicate)` - Get entities with filtering
- `GetAsync(predicate, orderBy, includeString)` - Advanced querying with ordering and includes
- `GetByIdAsync(id)` - Get entity by ID
- `AddAsync(entity)` - Add new entity
- `UpdateAsync(entity)` - Update existing entity
- `DeleteAsync(entity)` - Delete entity

## Future Enhancements
- Add validation using FluentValidation
- Implement event sourcing
- Add integration with message broker (RabbitMQ/Azure Service Bus)
- Implement saga pattern for distributed transactions
- Add logging with Serilog
- Implement health checks
