
# 📚 BakuKitabevi – E-Commerce Bookstore Platform

![Screenshot](https://github.com/user-attachments/assets/e2e2b11a-ed0c-447c-92c0-9aa2e1b8ee07)

> ⚠️ **Backend Status**: Offline (Hosting expired)  
> ✅ **Frontend**: Live on [www.bakukitabevi.com](https://www.bakukitabevi.com)

---

## ✨ Overview

**BakuKitabevi** is a scalable e-commerce platform focused on online book sales. Built with modern tools and best practices like **Clean Architecture**, **CQRS**, and **MediatR**, this project aims to provide an enterprise-level backend powered by **ASP.NET Core 8** and a responsive frontend on **Firebase**.

---

## 🔐 Hosting & Deployment Status

| Layer     | Status  | Hosting Provider      |
|-----------|---------|------------------------|
| Backend   | ❌ Offline | (was on SmartASP.NET) |
| Frontend  | ✅ Online  | Firebase              |
| Future    | 🔄 Migrating to Render / GCP / Azure |




## 🚀 Core Features

✅ User Authentication via Google OAuth 2.0  
✅ Book Management (CRUD)  
✅ Basket & Order System  
⏳ Payment Integration *(in progress)*  
✅ JWT & Refresh Token Security  
✅ Clean & Modular Architecture  
✅ Logging with Serilog + Seq  
✅ CORS, Rate Limiting, FluentValidation  
✅ Hosting-Ready Structure (Cloud-oriented)

The following technologies and libraries are utilized in this project:

- **ASP.NET Core 8**: The latest framework for building scalable and performant web applications.
- **Entity Framework Core**: An ORM (Object-Relational Mapper) for database access, providing a streamlined way to interact with databases.
- **MediatR**: A library for implementing CQRS (Command Query Responsibility Segregation) and decoupling commands and queries.
- **FluentValidation**: A library for input validation, ensuring consistent and maintainable validation logic across the project.
- **Google Cloud**: Cloud platform for scalable infrastructure, offering services for storage, computing, and other cloud-based solutions.
- **Serilog**: A structured logging library for centralized logging and monitoring of the application's runtime behavior.
- **Google OAuth 2.0**: A secure user authentication protocol using Google’s OAuth 2.0 for seamless sign-in and authorization.
- **JWT (JSON Web Tokens)**: For secure token-based authentication and authorization between the server and client.


---

## ⚠️ Notes for Future Maintenance & Improvements

> ℹ️ This project was implemented earlier and remains functional.  
> Before extending, refactoring, or deploying it to production, please review the notes below carefully.

---

### 🔌 API Design Considerations

- ⚠️ Some endpoints follow an **RPC-style naming convention**  
  _(e.g. `/createBook`, `/updateBook`)_
- ⚠️ Most endpoints return **`200 OK` regardless of operation type**

✅ **When maintaining or extending the API:**

- 🔁 Prefer **REST-style resource-based routes**
- 📡 Use proper HTTP status codes:
  - `201 Created` → create operations
  - `204 NoContent` → update / delete
  - `404 NotFound`, `400 BadRequest`, `409 Conflict` where applicable

---

### 📦 Response & Result Handling

- ⚠️ Multiple response/result models exist across the project
- ⚠️ Inconsistent naming detected (`Success` vs `Succes`)
  - This may affect validation and controller logic

✅ **Recommendation:**

- 🔧 Unify response contracts into a **single standard API result model**
- 🔗 Ensure **validation, controllers, and middleware** rely on the same contract

---

### 🧪 Validation & Error Handling

- ⚠️ Both **MediatR pipeline validation** and **ASP.NET automatic validation** are present
- ⚠️ Exception middleware currently exposes **raw exception messages**

✅ **When improving error handling:**

- 🎯 Prefer a **single validation strategy**
- 📄 Use standardized error responses (e.g. `ProblemDetails`)
- 🔒 Avoid exposing internal exception details in production

---

### 🔄 Transaction Usage

- ⚠️ Explicit database transactions are used where EF Core implicit transactions may suffice
- ⚠️ Authentication flow includes **external IO (email sending)** inside transactional logic

❗ **Important:**

- ⏱ Keep database transactions **short**
- 🚫 Avoid mixing external services (email, notifications) inside DB transactions
- 📤 Consider an **Outbox-style pattern** if reliability becomes critical

---

### 🧩 Data Model & Relationships

- ⚠️ `Basket` and `Order` entities contain **circular one-to-one references**
- ⚠️ This may complicate future schema changes or migrations

✅ **When refactoring:**

- ✂️ Simplify ownership boundaries
- 🧺 Treat `Basket` as a **temporary aggregate**
- 📄 Persist `Order` as an **independent entity** after checkout

---

### 🔐 Security Notes

- ⚠️ Sensitive tokens should **not be logged**
- ⚠️ Refresh tokens are stored in **plain text**
- ⚠️ Password policy is intentionally weak for development/demo purposes

✅ **Before production use:**

- 🔐 Hash refresh tokens
- 💪 Strengthen password requirements
- 🪵 Review logging configuration for sensitive data

---

### ✅ Summary for Maintainers

✔️ This project provides a **solid functional base**.  
⚠️ However, before **production use or major feature extensions**, the points above should be reviewed and addressed to improve:

- 🧱 Maintainability  
- 🔐 Security  
- 🏗 Architectural clarity

---



