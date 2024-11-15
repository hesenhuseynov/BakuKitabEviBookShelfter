Baku Kitabevi - E-Commerce Platform
Baku Kitabevi is a modern e-commerce platform built with ASP.NET Core 8, designed for online book sales. The project leverages a range of design patterns and architectural principles, including Clean Architecture, CQRS (Command Query Responsibility Segregation), Repository Pattern, MediatR, and FluentValidation. Additionally, the platform integrates with Google Cloud services, features Google Authentication, exception management, and cross-cutting concerns like logging and validation.

Key Features
ASP.NET Core 8: Utilizing the latest .NET technologies for optimal performance and security.
CQRS and MediatR: Separating command and query responsibilities to optimize reading and writing operations, utilizing MediatR for decoupling.
FluentValidation: All inputs and models are validated using FluentValidation for consistent and reusable validation logic.
Google Cloud Integration: Seamless integration with Google Cloud services for scalable storage and infrastructure.
Google Authentication: Secure user login using Google OAuth 2.0 authentication.
Cross-Cutting Concerns: Centralized exception handling, logging, caching, and validation are handled through well-defined services.
Clean Code Principles: Ensuring maintainable, readable, and clean code that adheres to SOLID principles.
Aspect-Oriented Programming (AOP): Implemented AOP techniques alongside OOP to decouple cross-cutting concerns, using custom aspects for logging, validation, and exception handling.
Technologies & Libraries
ASP.NET Core 8
Entity Framework Core (EF Core) for ORM-based database access
MediatR for CQRS
FluentValidation for input validation
Google Cloud Storage and other GCP services
Redis for caching
Serilog for centralized logging
Swagger for API documentation
Google OAuth 2.0 for user authentication
Architecture Overview
This project follows a Clean Architecture approach to ensure a clear separation of concerns and maintainability. Below are the key architectural layers:

API Layer: Handles incoming HTTP requests, validates user input, and returns API responses.
Application Layer: Contains the business logic, including commands, queries, validation, and the use of MediatR for handling CQRS.
Domain Layer: Defines core entities, aggregates, and domain logic.
Infrastructure Layer: Manages external dependencies such as EF Core, caching with Redis, and integrations with Google Cloud.
Project Structure
API: Contains controllers and endpoint definitions.
Application: Contains the application logic, implementing CQRS with MediatR for decoupled command and query handling.
Domain: Contains core business models, entities, and validation rules.
Infrastructure: Manages external dependencies like EF Core, Google Cloud, Redis, etc.
Getting Started
To get the project up and running locally, follow these steps:

Prerequisites
.NET 8 SDK
SQL Server (or another supported database)
Redis (for caching)
Google Cloud account (for GCP services)
Visual Studio 2022 or VS Code (or your preferred IDE)
Installation Steps
Clone the Repository:

bash
Copy code
git clone https://github.com/yourusername/baku-kitabevi.git
cd baku-kitabevi
Install NuGet Dependencies:

bash
Copy code
dotnet restore
Set up the Database:

Configure the connection string in appsettings.json and run the migration to create the database schema:
bash
Copy code
dotnet ef database update
Google Cloud Configuration:

Create a project in Google Cloud Platform.
Set up necessary credentials (API keys, storage, etc.) and add them to your appsettings.json.
Run the Application:

bash
Copy code
dotnet run
Access API Documentation:

Once the app is running, access the Swagger UI at https://localhost:5001/swagger to test and interact with the API.
