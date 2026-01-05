
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


------

## ⚠️ Important Maintenance Notes (Legacy Implementation)

> 🕰️ **Note:** This project was initially implemented several years ago and reflects the author's knowledge and experience at that time.  
> While the system remains functional, certain architectural and implementation decisions are now considered suboptimal.

> 🎯 The purpose of this section is to **explicitly document known issues and design shortcomings**,  
> so that anyone maintaining or extending this project can do so **with full awareness**.

---

### 🔌 API Design (Legacy Decisions)

- ⚠️ Some endpoints follow an **RPC-style naming convention**  
  _(e.g. `/createBook`, `/updateBook`)_
- ⚠️ HTTP status codes are often returned as `200 OK`, regardless of operation type

🛠 **If you plan to refactor or extend the API:**

- 🔁 Migrate towards **REST-style, resource-based routes**
- 📡 Apply proper HTTP status codes:
  - `201 Created` → create operations
  - `204 NoContent` → update / delete
  - `404 NotFound`, `400 BadRequest`, `409 Conflict` where applicable

---

### 📦 Response & Result Handling (Known Issues)

- ⚠️ Multiple response/result models exist across the project
- ⚠️ Inconsistent naming (`Success` vs `Succes`) may cause:
  - Validation inconsistencies
  - Controller logic errors

🛠 **Recommended action:**

- 🔧 Consolidate responses into a **single unified API result model**
- 🔗 Align validation, controllers, and middleware around this contract

---

### 🧪 Validation & Error Handling (Legacy Behavior)

- ⚠️ Both **MediatR pipeline validation** and **ASP.NET automatic validation** are present
- ⚠️ Exception middleware may expose **raw exception messages**

🛠 **Recommended action:**

- 🎯 Choose a **single validation strategy**
- 📄 Adopt standardized error responses (`ProblemDetails`)
- 🔒 Prevent leaking internal exception details in production

---

### 🔄 Transaction Handling (Potential Risks)

- ⚠️ Explicit transactions are used in cases where EF Core implicit transactions would suffice
- ⚠️ External IO (e.g. email sending) is performed inside transactional flows

🛠 **Recommended action:**

- ⏱ Reduce transaction scope and duration
- 🚫 Separate external services from database transactions
- 📤 Consider introducing an **Outbox Pattern** for reliability

---

### 🧩 Data Model Design (Design Flaws)

- ⚠️ Circular one-to-one relationships exist between `Basket` and `Order`
- ⚠️ This design complicates migrations and future schema changes

🛠 **Recommended action:**

- ✂️ Simplify aggregate ownership
- 🧺 Treat `Basket` as a **temporary shopping context**
- 📄 Persist `Order` as an **independent aggregate**

---

### 🔐 Security Considerations (Non-Production Defaults)

- ⚠️ Sensitive tokens may be logged
- ⚠️ Refresh tokens are stored in **plain text**
- ⚠️ Password policy is intentionally weak

🛠 **Recommended action before production use:**

- 🔐 Hash refresh tokens
- 💪 Strengthen password policies
- 🪵 Audit logging configuration for sensitive data

---

### ✅ Final Note

✔️ This repository intentionally preserves these notes to ensure **transparency**.  
✔️ The identified issues reflect **architectural evolution**, not negligence.  
✔️ Anyone maintaining or extending this project is encouraged to address these points first.

---






