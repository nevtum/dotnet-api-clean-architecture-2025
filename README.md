# .NET Web API Clean Architecture Example

## Overview
This is a .NET 10.0 web API project using ASP.NET Core, Entity Framework Core, and PostgreSQL. The project provides a basic web API with product management CRUD endpoints.

## Technologies
- .NET 10.0
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Swagger/OpenAPI

## Prerequisites
- .NET 10.0 SDK
- PostgreSQL Database

## Project Structure
- `src/api/`: Main Web API project
  - `Controllers/`: API endpoint controllers
  - `Services/`: Repository and business logic services
  - `Migrations/`: Database migration files
- `src/common/`: Shared project with common interfaces and models

## Features
- Product management API
- Swagger UI for API documentation

## Configuration
1. Configure database connection string in `appsettings.Development.json`
2. Run database migrations:
   ```bash
   dotnet ef database update
   ```

## Running the Application
```bash
# Navigate to the src/api directory
cd src/api

# Restore dependencies
dotnet restore

# Run the application
dotnet watch run
```

## Accessing Swagger UI
Open a browser and navigate to `/swagger` to view and test API endpoints.

## Database Migrations
To create a new migration:
```bash
dotnet ef migrations add MigrationName
```

To update the database:
```bash
dotnet ef database update
```
