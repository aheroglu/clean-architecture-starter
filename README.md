<h1 align="center">
  Clean Architecture Starter Template
</h1>


<p align="center">
  <img src="https://socialify.git.ci/aheroglu/clean-architecture-starter/image?description=1&amp;font=Inter&amp;language=1&amp;name=1&amp;owner=1&amp;pattern=Solid&amp;stargazers=1&amp;theme=Auto"
    alt="project-image">
</p>

## This is an starter template for easily start to develop your projects.

## Table Of Contents

- [Clean Architecture](#clean-architecture)    
  - [Give a start! :star:](#give-a-star-star)
  - [Versions :package:](#versions)
  - [Technologies Used :gear:](#technologies-used)
  - [Architecture :building_construction:](#architecture)
  - [Layers :card_index_dividers:](#layers)
  - [Packages :package:](#packages)
  - [Setup :gear:](#setup)
  - [Redis Cache :file_cabinet:](#redis-cache)
  - [Dummy Data :clown_face:](#dummy-data)
  - [Testing :heavy_check_mark:](#testing)
  - [Development :rocket:](#development)

## Give a start! :star:
Do you using or like this project? Please give a start for supporting me!

## Versions :package:
The project currently uses .NET version 8.

## Technologies Used :gear:
- .NET 8
- Angular
- Entity Framework Core
- Redis Cache
- MediatR
- FluentValidation
- Mapster
- Bogus (For dummy datas)
- xUnit (Testing)

## Architecture :building_construction:

This project follows the Clean Architecture principles, ensuring a strict separation of concerns between different layers. The aim is to provide a maintainable and scalable structure adhering to SOLID principles.

### Clean Architecture Layes
- <b>Domain</b>: Contains business rules and entities.
- <b>Application</b>: Contains application logic, services, CQRS, and validations.
- <b>Infrastructure</b>: Manages database connections, external services, and other infrastructure concerns.
- <b>Presentation</b>: Handles API and user interface logic (Controllers).
- <b>WebAPI</b>: Orchestrates the presentation layer of the application.

## Layers :card_index_dividers:

![Application Layers](https://resmim.net/cdn/2024/09/21/mowSjR.png)

### Domain Layer
- Abstractions (Base Entity)
- Entities (Domain entities)
- Repositories (Interfaces: IUnitOfWork, IRepository, IProductRepository)

### Application Layer
- Features (CQRS, Handlers using MediatR)
- Behaviors (Custom Validation Behavior with MediatR)
- Common (Generic Result Class)
- Services (IJwtProvider Interface)

### Infrastructure Layer
- Context (IdentityDbContext and UnitOfWork Implementation)
- Repositories (Repository classes implementing Domain repositories)
- Services (JWT implementation)
- Options (JWT Configuration: JwtOptions, JwtSetupOptions)
- Redis (Cache logic)

### Presentation Layer
- Base API Controller
- Controllers (handling API routes)

### Test Layer
- Unit tests using xUnit and Moq.

## Packages :package:
- MediatR
- FluentValidation
- AutoMapper
- StackExchange.Redis
- xUnit
- Moq
- Bogus

## Setup :gear:
1. Clone the repository:
```csharp
git clone https://github.com/aheroglu/clean-architecture-starter.git
```

2. Install the necessary NuGet dependencies:
```csharp
dotnet restore
```

3. Apply the database migrations:
```csharp
dotnet ef database update
```

4. Run the project:
```csharp
dotnet run
```

## Redis Cache :file_cabinet:

![Application Layers](https://resmim.net/cdn/2024/09/21/mowZ0j.png)

The project is integrated with Redis Cache. Add Redis configuration to the appsettings.json file:
```json
"CacheOptions": {
    "ConnectionString": "localhost,abortConnect=false",
    "InstanceName": "Redis_Server"
}
```
Redis is configured using the CacheService class, which implements the ICacheService interface to handle caching.

## Dummy Data :clown_face:
The Bogus library is used to generate dummy data through the Helper class. The application will populate the database with random data when it starts in development mode.

In ```Program.cs```, the dummy data generation is triggered like this:
```csharp
if (builder.Environment.IsDevelopment())
{
    Helper.GenerateData(app).Wait();
}
```

## Testing :heavy_check_mark:
Unit tests are written using xUnit and Moq. Tests cover various CRUD operations and logic through mock repositories and services.
```csharp
dotnet test
```

## Development :rocket:
You can continue developing the project by following the layered architecture. For new features, you can add CQRS handlers to the Application layer and define any necessary services in the Infrastructure layer.

![Application Layers](https://resmim.net/cdn/2024/09/21/mowfJn.png)
