<h1 align="center">
  Clean Architecture Starter Template
</h1>

<p align="center">
  <img src="https://socialify.git.ci/aheroglu/clean-architecture-starter/image?description=1&language=1&name=1&owner=1&pattern=Solid&stargazers=1&theme=Auto"
    alt="project-image">
</p>

<h2 align="center">
  This is an starter template for easily start to develop your projects.
</h2>

## Table Of Contents

- [Clean Architecture](#clean-architecture)    
  - [Give a start! :star:](#give-a-star-star)
  - [Versions :package:](#versions)
  - [Technologies Used :gear:](#technologies-used)
  - [Architecture :building_construction:](#architecture)
  - [Layers :card_index_dividers:](#layers)
  - [Packages :package:](#packages)
  - [Setup :gear:](#setup)
  - [Dummy Data :clown_face:](#dummy-data)
  - [Testing :heavy_check_mark:](#testing)
  - [Development :rocket:](#development)

## Give a star! :star:
Do you using or like this project? Please give a star for supporting me!

## Versions :package:
The project currently uses .NET version 8.

## Technologies Used :gear:
- .NET 8
- Entity Framework Core
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
- Services (JWT implementation, Cache implementation)
- Options (JwtOptions, JwtSetupOptions)

### Presentation Layer
- Base API Controller
- Controllers (handling API routes)

### Test Layer
- Unit tests using xUnit and Moq.

## Packages :package:
- MediatR
- FluentValidation
- AutoMapper
- xUnit
- Moq
- Bogus

## Setup :gear:
1. Clone the repository:
```powershell
git clone https://github.com/aheroglu/clean-architecture-starter.git
```

2. Install the necessary NuGet dependencies:
```powershell
dotnet restore
```

3. Apply the database migrations:
```powershell
dotnet ef database update
```

4. Run the project:
```powershell
dotnet run
```

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
