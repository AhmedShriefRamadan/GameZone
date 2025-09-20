# GameZone

GameZone is an ASP.NET Core MVC application built with **.NET 8**.  
It demonstrates full **CRUD operations** with Entity Framework Core, client-side validation, and modern UI enhancements like **Select2** and **SweetAlert**.

---

## Table of Contents

- [GameZone](#gamezone)
  - [Table of Contents](#table-of-contents)
  - [Features](#features)
  - [Tech Stack](#tech-stack)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)

---

## Features

- Create, Read, Update, and Delete (CRUD) operations for Games
- Entity Framework Core integration with code-first approach
- Validation (server-side & client-side with custom attributes)
- File upload with extension & size restrictions
- Options pattern for configuration
- Enhanced UI:
  - Bootstrap & custom themes
  - Select2 for dropdowns
  - SweetAlert confirmation dialogs
  - Bootstrap icons
- Ajax-based delete operations
- Seeded database with initial data

---

## Tech Stack

- **Framework:** ASP.NET Core MVC (.NET 8)  
- **ORM:** Entity Framework Core  
- **Database:** MySQL
- **Frontend:** (Razor views / HTML/CSS / any JS libraries)  

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed  
- A database server (if SQL Server) or appropriate setup for whatever DB you picked  
- IDE / Code Editor (Visual Studio / VS Code / Rider, etc.)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/GameZone.git
   cd GameZone
   ```

2. Configure the **connection string** in appsettings.json.
3. Apply migrations:

   ```bash
   dotnet ef database update
   ```

4. Run the application:

    ```bash
    dotnet run
    ```
