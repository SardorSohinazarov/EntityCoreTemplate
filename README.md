# EntityCoreTemplate

**EntityCoreTemplate** is a project template for building ASP.NET Core applications based on the **Clean Architecture** principles. It provides a well-structured starting point for developing maintainable, scalable, and testable web applications.

## Projects Generated

The template generates the following projects:

-   **Api**: Contains the ASP.NET Core Web API, including controllers, DTOs, and API-specific configurations. This is the presentation layer for API clients.
-   **Application**: Implements the application logic, including services, use cases, and commands/queries. It orchestrates the domain layer and infrastructure services.
-   **Domain**: Contains the core business logic, including entities, value objects, and domain events. This layer is independent of any specific technology or framework.
-   **Infrastructure**: Provides implementations for external concerns such as data access (Entity Framework Core), external services, and other infrastructure-related components.
-   **UI**: An ASP.NET Core Blazor Server project providing a web user interface for the application. This is an alternative presentation layer.

## Prerequisites

-   .NET SDK 8.0 or later.

## How to Install

### Install from Local Path

To install the template from a local directory (e.g., after cloning this repository):

```bash
dotnet new install ./
```

### Install from NuGet.org

Once published to NuGet.org, you will be able to install it using:

```bash
dotnet new install EntityCoreTemplate
```

*(Note: This template needs to be published to NuGet.org for this command to work.)*

## How to Use

To create a new project using the EntityCoreTemplate:

```bash
dotnet new entitycore -n YourProjectName
```

-   `-n YourProjectName`: Specifies the name of the new project to be created.

## Architecture Overview

This template follows the principles of **Clean Architecture**, promoting a separation of concerns and a dependency rule where inner layers (Domain, Application) do not depend on outer layers (Infrastructure, Presentation).

-   **Domain Layer**: The heart of the application. It contains business entities and core logic, independent of any specific technology.
-   **Application Layer**: Contains application-specific logic, orchestrating tasks by using domain entities and interfaces defined within this layer. Implementations of these interfaces are provided by the Infrastructure layer.
-   **Infrastructure Layer**: Handles external concerns like database access (using Entity Framework Core), file systems, network calls, etc. It implements interfaces defined in the Application layer.
-   **Presentation Layer (Api/UI)**: The outermost layer, responsible for user interaction (UI) or client interaction (API). It depends on the Application layer to execute user requests or API calls.

## Key Features

-   **ASP.NET Core API**: A ready-to-use Web API project with Swagger/OpenAPI documentation.
-   **ASP.NET Core Blazor Server UI**: A Blazor Server project for a rich, interactive web UI.
-   **Entity Framework Core**: Pre-configured for data access, using SQL Server by default.
-   **Auditable Entities**: Automatic tracking of `Created`, `CreatedBy`, `LastModified`, and `LastModifiedBy` properties for entities inheriting from `Auditable`.
-   **Dependency Injection**: Properly set up using .NET Core's built-in DI container.
-   **Basic CRUD Example**: Includes a "Books" entity with sample CRUD operations to demonstrate the architecture.

## Configuration

### Database Connection

By default, the template is configured to use **SQL Server LocalDB**. You will need to update the connection string in the `appsettings.json` files of the **Api** and **UI** projects.

**Example `appsettings.json`:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourProjectNameDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    // ...
  },
  "AllowedHosts": "*"
}
```

Replace `"YourProjectNameDb"` with your desired database name.

### Other Configurations

Review `appsettings.json` in the `Api` and `UI` projects for other configurable settings such as logging levels.

## Getting Started

### Database Migrations

The template uses Entity Framework Core migrations to manage the database schema.

**1. Apply Initial Migrations:**

To create the database and apply the initial migrations, open a terminal in the root directory of your solution and run:

```bash
dotnet ef database update --project src/EntityCoreTemplate.Infrastructure --startup-project src/EntityCoreTemplate.Api
```

If you prefer to manage migrations primarily through the UI project (ensure it's configured to handle EF Core tools):

```bash
dotnet ef database update --project src/EntityCoreTemplate.Infrastructure --startup-project src/EntityCoreTemplate.UI
```

**2. Add New Migrations:**

When you make changes to your domain entities, you'll need to add a new migration:

```bash
dotnet ef migrations add MyNewMigrationName --project src/EntityCoreTemplate.Infrastructure --startup-project src/EntityCoreTemplate.Api
```

(Replace `MyNewMigrationName` with a descriptive name for your migration, e.g., `AddAuthorEntity`.)
Then, apply the new migration using the `database update` command shown above.

### Running the Application

**Running the API Project:**

Navigate to the `src/EntityCoreTemplate.Api` directory and run:

```bash
dotnet run
```

By default, the API will be accessible at `https://localhost:PORT_API/swagger` (e.g., `https://localhost:7001/swagger`) where you can explore and test the API endpoints.

**Running the UI Project:**

Navigate to the `src/EntityCoreTemplate.UI` directory and run:

```bash
dotnet run
```

By default, the Blazor Server UI will be accessible at `https://localhost:PORT_UI` (e.g., `https://localhost:7002`).

*(Note: The actual ports are defined in `Properties/launchSettings.json` for each project.)*

## Testing

It is strongly recommended to add dedicated test projects to your solution to ensure the reliability and maintainability of your application. Consider using common .NET testing frameworks such as xUnit, NUnit, or MSTest.

Here's brief guidance on the types of tests suitable for different layers:

-   **Domain Layer**:
    -   Write unit tests for your domain entities, value objects, and domain services.
    -   Focus on validating business logic, rules, and invariants.
    -   These tests should be independent of any infrastructure concerns.

-   **Application Layer**:
    -   Write unit tests for your application services, command handlers, and query handlers.
    -   Mock dependencies such as repositories, external service clients, or other application services.
    -   Focus on testing the orchestration of domain objects and application-specific logic.

-   **Infrastructure Layer**:
    -   Write integration tests for your repository implementations, database interactions, and any other integrations with external services (e.g., payment gateways, email services).
    -   These tests may require a test database, a running instance of an external service, or appropriate test doubles (e.g., using `Microsoft.EntityFrameworkCore.InMemory` for EF Core, or tools like Testcontainers).

-   **API/UI Layers (Presentation)**:
    -   **API**: Write integration tests for your API controllers and endpoints. These tests will typically involve sending HTTP requests to your API and asserting the responses. Consider using `Microsoft.AspNetCore.Mvc.Testing` for in-memory testing of your API.
    -   **UI**:
        -   For Blazor components, write component tests to verify their behavior in isolation or with minimal dependencies.
        -   Consider end-to-end (E2E) tests for critical user flows using tools like Playwright or Selenium, which automate browser interactions.

Adding a comprehensive test suite will help catch regressions, improve code quality, and make refactoring safer.

## Potential Enhancements

This template provides a solid foundation, but there are several ways it could be enhanced further:

-   **Authentication & Authorization**:
    -   The current template includes minimal authentication/authorization setup. A robust solution using ASP.NET Core Identity, JWT tokens (bearer authentication for the API), or integration with an identity provider (e.g., IdentityServer, OpenIddict, Azure AD B2C, Auth0) would be a significant improvement for real-world applications.

-   **UI Interaction Pattern**:
    -   The Blazor Server UI currently interacts directly with Application layer services. For larger applications, improved team workflow separation, or if the UI were a separate SPA, having the UI communicate with the API project (which would act as a Backend-For-Frontend - BFF) is a common alternative pattern. This would also allow the API to serve multiple types of clients (e.g., web UI and mobile apps).

-   **`AddCustomServices` and Service Registration**:
    -   The template uses custom attributes (`ScopedService`, `TransientService`, `SingletonService`) in the Application layer for automatic service registration via the `AddCustomServices` extension method. While this can be convenient, users new to the template might need to familiarize themselves with this pattern. Adding more comments in the `DependencyInjection.cs` of the Application layer or a brief mention in this README could improve clarity.

-   **Template Parameterization (dotnet new options)**:
    -   Enhance the `dotnet new` experience by adding template parameters:
        -   `--database`: Choice of database provider (e.g., `SQLServer`, `PostgreSQL`, `SQLite`). This would require conditional inclusion of NuGet packages and potentially different EF Core configurations.
        -   `--no-ui`: Option to exclude the `EntityCoreTemplate.UI` project if only an API is needed.
        -   `--no-api`: Option to exclude the `EntityCoreTemplate.Api` project, perhaps if the UI is intended to be self-contained or use a different backend (less common with this architecture but possible).
        -   `--ui-framework`: (Major change) Option for different UI frameworks (e.g., Blazor WASM, React, Angular), though this would significantly increase template complexity.

-   **Automated Test Projects**:
    -   While the "Testing" section above recommends adding test projects, the template could be enhanced to optionally scaffold basic xUnit/NUnit/MSTest projects for one or more layers (e.g., Domain, Application) with example tests.

These are just a few ideas, and the best enhancements will depend on the evolving needs of .NET developers and common use cases for this type of architecture.

## Contribution

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request to the repository where this template is hosted.