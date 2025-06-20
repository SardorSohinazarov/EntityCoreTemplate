# EntityCoreTemplate

**EntityCoreTemplate** is a project template for building ASP.NET Core applications based on the **Clean Architecture** principles. It provides a well-structured starting point for developing maintainable, scalable, and testable web applications.

## Prerequisites

-   .NET SDK 8.0 or later.

## How to Install

Once published to NuGet.org, you will be able to install it using:

```cmd
dotnet new install EntityCoreTemplate::1.0.3
```

---

## Quick Start

Get started quickly with the EntityCoreTemplate using just a few commands:

```cmd
dotnet new entitycore -n YourProjectName
cd YourProjectName
dotnet run --project src/YourProjectName.Api
dotnet run --project src/YourProjectName.UI
```

## How to Use

To create a new project using the EntityCoreTemplate:

```bash
dotnet new entitycore -n YourProjectName
```

-   `-n YourProjectName`: Specifies the name of the new project to be created.

## Key Features

-   **ASP.NET Core API**: A ready-to-use Web API project with Swagger/OpenAPI documentation.
-   **ASP.NET Core Blazor Server UI**: A Blazor Server project for a rich, interactive web UI.
-   **Entity Framework Core**: Pre-configured for data access, using SQL Server by default.
-   **Auditable Entities**: Automatic tracking of `Created`, `CreatedBy`, `LastModified`, and `LastModifiedBy` properties for entities inheriting from `Auditable`.
-   **Dependency Injection**: Properly set up using .NET Core's built-in DI container.
-   **Basic CRUD Example**: Includes a "Books" entity with sample CRUD operations to demonstrate the architecture.

### Database Connection

By default, the template is configured to use **SQL Server LocalDB**. You will need to update the connection string in the `/YourProjectName/src/YourProjectName.Infrastucture/YourProjectNameDb.Options.cs` file.

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    // Default database Sql Server
    optionsBuilder.UseSqlServer(connectionString: "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EntityCoreTemplateDb;");
    
    // Sqliteda filelarda saqlangani uchun va birnechta startup projectlar(EntityCoreTemplate.Api va EntityCoreTemplate.UI) bo'lgani uchun maqul topmadim.
    //optionsBuilder.UseSqlite($"Data Source=EntityCoreTemplateDb");
}
```

Replace `"YourProjectNameDb"` with your desired database name.

### Other Configurations

Review `appsettings.json` in the `Api` and `UI` projects for other configurable settings such as logging levels.

### Database Migrations

The template uses Entity Framework Core migrations to manage the database schema.

**1. Apply Initial Migrations:**

To create the database and apply the initial migrations, open a terminal in the root directory of your solution and run:

```bash
dotnet ef database update --project src/YourProjectName.Infrastructure --startup-project src/YourProjectName.Api
```

If you prefer to manage migrations primarily through the UI project (ensure it's configured to handle EF Core tools):

```bash
dotnet ef database update --project src/YourProjectName.Infrastructure --startup-project src/YourProjectName.Api
```

**2. Add New Migrations:**

When you make changes to your domain entities, you'll need to add a new migration:

```bash
dotnet ef migrations add MyNewMigrationName --project src/YourProjectName.Infrastructure --startup-project src/YourProjectName.Api
```

(Replace `MyNewMigrationName` with a descriptive name for your migration, e.g., `AddAuthorEntity`.)
Then, apply the new migration using the `database update` command shown above.

### Running the Application

**Running the API Project:**

Navigate to the `src/YourProjectName.Api` directory and run:

```bash
dotnet run
```

By default, the API will be accessible at `https://localhost:PORT_API/swagger` (e.g., `https://localhost:7001/swagger`) where you can explore and test the API endpoints.

**Running the UI Project:**

Navigate to the `src/YourProjectName.UI` directory and run:

```bash
dotnet run
```

By default, the Blazor Server UI will be accessible at `https://localhost:PORT_UI` (e.g., `https://localhost:7002`).

*(Note: The actual ports are defined in `Properties/launchSettings.json` for each project.)*

## Contribution

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request to the repository where this template is hosted.