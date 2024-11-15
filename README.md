
Anladım, yani daha profesyonel, ileri düzeyde bir İngilizce README dosyası yazalım. İlgili tüm terimleri ve projeye dair teknik detayları içerecek şekilde hazırlayacağım. Ayrıca, görsel eklemeleri de nasıl yapabileceğini belirteceğim. İşte yazının yeni hali:

Baku Kitabevi - E-Commerce Platform
Baku Kitabevi is a modern e-commerce platform built with ASP.NET Core 8, designed for online book sales. The project utilizes a range of design patterns and architectural principles, including Clean Architecture, CQRS (Command Query Responsibility Segregation), Repository Pattern, MediatR, and FluentValidation. The platform also integrates with Google Cloud services and includes features such as Google Authentication, exception management, and cross-cutting concerns like logging and validation.

Key Features
ASP.NET Core 8: Leveraging the latest .NET technologies for optimal performance and security.
CQRS and MediatR: Separate command and query responsibilities to optimize reading and writing operations, using the MediatR library for decoupling.
FluentValidation: All inputs and models are validated using FluentValidation for consistent and reusable validation logic.
Google Cloud Integration: Seamless integration with Google Cloud services for scalable storage and infrastructure.
Google Authentication: Secure user login using Google OAuth 2.0 authentication.
Cross-Cutting Concerns: Centralized exception handling, logging, caching, and validation are handled through well-defined services.
Clean Code Principles: Focus on maintainable, readable, and clean code adhering to SOLID principles.
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
This project follows a Clean Architecture approach, ensuring a clear separation of concerns and maintainability. Below are the key architectural layers:

API Layer: Responsible for handling incoming HTTP requests, validating user input, and returning API responses.
Application Layer: Contains the application logic, including business rules, commands, queries, and validation. Uses MediatR for handling CQRS.
Domain Layer: Defines the core entities, aggregates, and domain logic.
Infrastructure Layer: Includes external dependencies such as database access (via EF Core), caching (via Redis), external services, and integrations (such as Google Cloud).
Project Structure
API: Contains controllers and endpoint definitions.
Application: The heart of the business logic, implementing CQRS with MediatR for decoupled command and query handling.
Domain: Contains core business models and validation rules.
Infrastructure: Manages external dependencies like EF Core, Google Cloud, Redis, etc.
Getting Started
To get the project up and running locally, follow these steps:

Prerequisites
.NET 8 SDK
SQL Server (or any other supported database)
Redis (for caching)
Google Cloud account for GCP services
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
Set up the Database: Configure the connection string in appsettings.json and run the migration to create the database schema:

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
Access API Documentation: Once the app is running, access the Swagger UI at https://localhost:5001/swagger to test and interact with the API.

Example API Endpoints
1. Get All Books
GET /api/books
Description: Retrieve a list of all available books.
Example Response:
json
Copy code
[
  {
    "id": 1,
    "title": "The Great Gatsby",
    "author": "F. Scott Fitzgerald",
    "price": 19.99
  },
  ...
]
2. Add a New Book
POST /api/books
Description: Adds a new book to the catalog.
Request Body:
json
Copy code
{
  "title": "New Book Title",
  "author": "Author Name",
  "price": 25.99
}
Cross-Cutting Concerns
This project implements key cross-cutting concerns using services and design patterns:

Exception Handling: Global exception handling is applied through middleware to catch and respond to exceptions in a consistent manner.
Logging: Centralized logging with Serilog, capturing relevant application logs, errors, and debugging information.
Validation: FluentValidation is used for input validation at both the API and service layers to ensure data consistency.
Authentication
Google OAuth 2.0: The project uses Google Authentication for user login. Configure the Google OAuth credentials in appsettings.json and enable Google Login in the application.
Clean Code & SOLID Principles
The project adheres to SOLID principles to ensure the code is modular, maintainable, and scalable:

Single Responsibility Principle: Each class and method has one clear responsibility.
Open/Closed Principle: The code is open for extension but closed for modification.
Liskov Substitution Principle: Derived classes can be used interchangeably with base classes.
Interface Segregation Principle: Interfaces are focused and don’t force implementing classes to use unnecessary methods.
Dependency Inversion Principle: High-level modules do not depend on low-level modules, but both depend on abstractions.
AOP (Aspect-Oriented Programming)
The project leverages AOP for handling common tasks such as logging, exception handling, and validation in a decoupled manner. This reduces code duplication and increases maintainability.
Testing
Unit and integration tests are included to ensure the application behaves as expected.

Running Tests:
bash
Copy code
dotnet test
Contributing
If you would like to contribute to this project, follow these steps:

Fork the repository.
Create a new branch for your changes.
Make your changes and test them thoroughly.
Submit a pull request with a description of the changes you’ve made.
