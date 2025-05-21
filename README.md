# FitnessWeb Server

This is the backend API for the [FitnessWeb UI](https://github.com/FitnessWebas/fitnessweb-ui), built with **.NET 8**, **Entity Framework Core**, and **MS SQL Server**.

## Tech stack

- [.NET 8](https://dotnet.microsoft.com/)
- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [Swashbuckle / Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- IDE (e.g., [Visual Studio](https://visualstudio.microsoft.com/) or [Rider](https://www.jetbrains.com/rider/))

### Installation

```bash
# Clone the repository
git clone https://github.com/FitnessWebas/fitnessweb-server.git
cd fitnessweb-server
```

### Configuration

Create an `appsettings.json` file following the example provided in `appsettings.Example.json`:

## Running the back-end
```bash
dotnet run
```
## 📁 Project Structure

```bash
fitnessWeb.Api/
├── wwwroot/               # Pictures for the exercises
├── Controllers/           # Endpoints
├── appsettings.json       # Configuration file
├── Program.cs             # Project startup file

fitnessWeb.Core/
├── Commands/              # Commands used in the endpoints
├── Handlers/              # Endpoints
├── Helpers/               # Helper files
├── Queries/               # Queries used in the endpoints
├── Services/              # Service files

fitnessWeb.Domain/
├── Dtos/                  # Dtos used in the endpoints
├── Entities/              # Database entities
├── Types/                 # Enums used in our project

fitnessWeb.Infrastructure/
├── Migrations/            # Migrations used to code-first create the database
├── FitnessWebDbContext.cs # DbContext class of our project
```
