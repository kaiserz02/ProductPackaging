# Product Packaging System API

## Overview

This project is a RESTful API built with **.NET** and **Microsoft SQL Server** that manages products and their hierarchical packaging structure.

It supports:

* JWT-based authentication
* API versioning (v1 and v2)
* Hierarchical packaging (nested structure)
* Structured logging
* Swagger documentation

---

## Tech Stack

* Backend: .NET (C#)
* Database: Microsoft SQL Server
* Authentication: JWT
* Logging: Serilog
* API Documentation: Swagger

---

## Features

### Authentication

* `POST /api/v1/auth/register` – Register user
* `POST /api/v1/auth/login` – Login and get JWT token

---

### Products

#### v1

* `GET /api/v1/products`

  * Returns product list
  * No packaging included

#### v2

* `GET /api/v2/products`

  * Returns products with full packaging hierarchy

---

## Sample API Response (v2)

```json
[
  {
    "productID": 1,
    "productName": "Self Adjusting Table",
    "packages": [
      {
        "packageID": 1,
        "packageTypeName": "Box",
        "packages": [...]
      }
    ]
  }
]
```

---

## How to Run

1. Clone repository
2. Update connection string in `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ProductPackagingDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. Add JWT key

```json
"Jwt": {
  "Key": "THIS_IS_A_SECRET_KEY_12345"
}
```

4. Run the project

```bash
dotnet run
```

5. Open Swagger

```
https://localhost:7041/swagger
```

---

## Database Design

### Tables

* Products
* Packaging
* PackagingTypes
* Items
* PackagingItems
* Users

---

### Relationships

* Product → Packaging (1 to many)
* Packaging → Packaging (self-referencing hierarchy)
* Packaging → Items (many-to-many via PackagingItems)
* Packaging → PackagingTypes (many-to-one)

---

### ERD (Simplified)

```
Products (1) ──── (Many) Packaging
Packaging (1) ──── (Many) Packaging (ParentPackageId)
Packaging (Many) ──── (Many) Items
Packaging (Many) ──── (1) PackagingTypes
```

---

## Hierarchical Query (SQL)

```sql
WITH PackagingHierarchy AS (
    SELECT PackageId, ParentPackageId, 0 AS Level
    FROM Packaging
    WHERE ParentPackageId IS NULL

    UNION ALL

    SELECT c.PackageId, c.ParentPackageId, ph.Level + 1
    FROM Packaging c
    JOIN PackagingHierarchy ph ON c.ParentPackageId = ph.PackageId
)
SELECT * FROM PackagingHierarchy;
```

---

## Key Design Decisions

* Used self-referencing table for hierarchical packaging
* Used DTOs to separate API responses from database entities
* Implemented API versioning for backward compatibility
* Used recursive logic in API to build nested structure

---

## Logging

* Implemented using Serilog
* Logs stored in file (`/logs/app.log`)
* Captures:

  * Authentication events
  * API requests
  * Errors

---

## Notes

* v1 API returns simple product data
* v2 API returns full hierarchical packaging
* Swagger is enabled for testing endpoints


