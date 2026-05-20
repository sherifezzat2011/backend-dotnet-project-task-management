# Project Task Management API

A scalable backend API for managing projects and tasks, built as a technical assessment for a Backend .NET Developer role.

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / LocalDB
- JWT Authentication
- Clean Architecture
- xUnit
- Swagger
- MediatR

## Solution Structure

- `src/ProjectTaskManagement.API`
  HTTP layer, controllers, middleware, Swagger, and application startup.
- `src/ProjectTaskManagement.Application`
  Use cases, DTOs, validation, abstractions, mapping, application exceptions, and CQRS command/query handlers.
- `src/ProjectTaskManagement.Domain`
  Core entities and enums.
- `src/ProjectTaskManagement.Infrastructure`
  EF Core persistence, repositories, JWT generation, password hashing, and dependency wiring.
- `tests/ProjectTaskManagement.Tests`
  Unit tests for application services.

## Implemented Features

### Authentication

- Register user
- Login user
- JWT token generation

### Projects

- Create project
- Get all authenticated user projects
- Get project by id
- Update project
- Delete project

### Tasks

- Create task inside a project
- Get tasks by project
- Update task status
- Delete task

### Cross-Cutting

- Clean Architecture layering
- Dependency Injection
- DTO usage
- Validation
- Global exception handling
- Generic response wrapper
- User ownership isolation for projects and tasks
- CQRS with MediatR

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server or LocalDB

### Configuration

Default configuration is in [appsettings.json](src/ProjectTaskManagement.API/appsettings.json).

Update these if needed:

- `ConnectionStrings:DefaultConnection`
- `Jwt:Issuer`
- `Jwt:Audience`
- `Jwt:SecretKey`

### Run Database Migration

```bash
dotnet ef database update --project src/ProjectTaskManagement.Infrastructure --startup-project src/ProjectTaskManagement.API
```

### Run the API

```bash
dotnet run --project src/ProjectTaskManagement.API
```

The default development URLs are:

- `http://localhost:5063`
- `https://localhost:7011`

### Swagger

When running in Development, Swagger UI is available at:

- `https://localhost:7011/swagger`
- `http://localhost:5063/swagger`

## Sample Endpoints

### Auth

- `POST /api/auth/register`
- `POST /api/auth/login`

### Projects

- `GET /api/projects`
- `GET /api/projects/{id}`
- `POST /api/projects`
- `PUT /api/projects/{id}`
- `DELETE /api/projects/{id}`

### Tasks

- `GET /api/projects/{projectId}/tasks`
- `POST /api/projects/{projectId}/tasks`
- `PATCH /api/tasks/{id}/status`
- `DELETE /api/tasks/{id}`

## Tests

Run tests with:

```bash
dotnet test
```

## Notes

- The project uses `ProjectTask` and `ProjectTaskStatus` naming internally to avoid conflicts with built-in .NET `Task` and `TaskStatus`.
- API requests are routed through MediatR handlers using a CQRS-style split between commands and queries.
- Migration files are included under `src/ProjectTaskManagement.Infrastructure/Persistence/Migrations`.
- Swagger is provided instead of a Postman collection as allowed by the task requirements.
