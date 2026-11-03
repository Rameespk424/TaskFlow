# TaskFlow

A task management REST API built with **.NET 8**, **Clean Architecture**, **Dapper** and **SQL Server stored procedures**.

TaskFlow is a learning project: every layer, from the database schema to the HTTP controller, is written by hand, so each design decision can be explained and defended.

---

## Tech stack

| Area | Technology |
|---|---|
| API | ASP.NET Core Web API (.NET 8) |
| Data access | Dapper, stored procedures only (no inline SQL) |
| Database | SQL Server (LocalDB for development) |
| API docs | Swagger (Swashbuckle) |
| Architecture | Clean Architecture, four layers |

---

## Architecture

```
TaskFlow.API             → Controllers, exception middleware, DI registration
   │
   ▼
TaskFlow.Application     → TaskService, ITaskService, DTOs
   │
   ▼
TaskFlow.Domain          → Entities, enums, repository interfaces   (depends on nothing)
   ▲
   │
TaskFlow.Infrastructure  → Dapper repositories, SQL connection factory
```

Dependencies point **inward**, and the project references enforce it:

| Project | References |
|---|---|
| `TaskFlow.Domain` | nothing |
| `TaskFlow.Application` | Domain |
| `TaskFlow.Infrastructure` | Domain |
| `TaskFlow.API` | Application, Infrastructure |

`TaskFlow.Application` has **no reference** to Infrastructure, so the service layer cannot use Dapper or SQL types even by accident. Dapper and `Microsoft.Data.SqlClient` are installed only in Infrastructure.

`TaskFlow.Playground` is a small console harness used during early development. It is not part of the API.

### Request flow

```
HTTP request
  → ExceptionHandlingMiddleware   (outermost: turns exceptions into error responses)
  → TaskController                (HTTP only: binds the request, maps results to status codes)
  → ITaskService / TaskService    (business rules and validation)
  → ITaskRepository / TaskRepository  (Dapper, async)
  → usp_* stored procedure        (SQL Server)
```

Every class depends on an **interface**, and `Program.cs` maps each interface to its implementation:

| Interface | Implementation | Lifetime |
|---|---|---|
| `ISqlConnectionFactory` | `SqlConnectionFactory` | Singleton |
| `ITaskRepository` | `TaskRepository` | Scoped |
| `IProjectRepository` | `ProjectRepository` | Scoped |
| `ITaskService` | `TaskService` | Scoped |

---

## Getting started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server **LocalDB** (installed with Visual Studio), or any SQL Server instance
- Windows, if you use LocalDB. On macOS or Linux, use a full SQL Server instance and change the connection string (step 3).
- SQL Server Management Studio (SSMS) or `sqlcmd` to run the database scripts

### 1. Clone the repository

```bash
git clone https://github.com/Rameespk424/TaskFlow.git
cd TaskFlow
```

### 2. Create the database

Create an empty database:

```sql
CREATE DATABASE TaskFlow;
```

Then run the scripts in the `Database/` folder **in this order**:

1. `01_Schema.sql`: tables
2. `02_ForeignKeys.sql`: foreign keys
3. `03_SeedData.sql`: one user and one project, so you can create tasks straight away
4. All `usp_*.sql` files: the stored procedures (any order)

In SSMS, open each file and press **Execute**. Or from a terminal:

```bash
sqlcmd -S "(localdb)\MSSQLLocalDB" -d TaskFlow -i Database\01_Schema.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -d TaskFlow -i Database\02_ForeignKeys.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -d TaskFlow -i Database\03_SeedData.sql
```

In SSMS, make sure the database dropdown says **TaskFlow** before you execute, not `master`.

`01_Schema.sql` and `02_ForeignKeys.sql` are for an **empty** database. The seed script and the stored procedures are **safe to run more than once**: procedures use `CREATE OR ALTER`, and the seed script checks before inserting.

See [Database/README.md](Database/README.md) for details of each script.

### 3. Check the connection string

The connection string is in `TaskFlow.API/appsettings.json`, under the key `TaskFlowDb`:

```json
"ConnectionStrings": {
  "TaskFlowDb": "Server=(localdb)\\MSSQLLocalDB;Database=TaskFlow;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

It uses Windows authentication, so there is no password. If you are not using LocalDB, change the `Server=` value to your SQL Server instance.

### 4. Run the API

**Visual Studio:** open `TaskFlow.sln`, set `TaskFlow.API` as the startup project, select the `https` profile next to the ▶ button, and press **F5**.

**Terminal:**

```bash
dotnet run --project TaskFlow.API --launch-profile https
```

Without `--launch-profile https`, `dotnet run` uses the `http` profile and the API runs on `http://localhost:5124` instead.

### 5. Try it in Swagger

Swagger opens automatically at:

```
https://localhost:7044/swagger
```

Find the seeded project's Id:

```sql
SELECT Id FROM Projects WHERE Name = 'SEEDED Project';
```

Then use **POST /api/Task** with that Id as `projectId`, and fetch the new task with **GET /api/Task/{id}**.

---

## API endpoints

| Method | URL | Success | Errors |
|---|---|---|---|
| `POST` | `/api/Task` | `201 Created` with the new Id | `400` invalid data or project not found |
| `GET` | `/api/Task/{id}` | `200 OK` with the task | `404` task not found |
| `PUT` | `/api/Task/{id}` | `204 No Content` | `400` invalid data, `404` task not found |
| `DELETE` | `/api/Task/{id}` | `204 No Content` | `404` task not found |
| `PUT` | `/api/Task/{id}/project` | `204 No Content` | `400` project not found, `404` task not found |

Errors are returned as JSON:

```json
{ "error": "Project 9999 does not exist." }
```

---

## Design decisions

**Stored procedures with Dapper, not an ORM.**
All data access goes through stored procedures. The SQL is explicit and easy to tune, and Dapper maps results to objects with very little overhead. Create returns the new Id through `SCOPE_IDENTITY()`; update and delete return `@@ROWCOUNT`, which the API turns into `204` or `404`.

**Validation in two layers, with the database as the final guarantee.**
- Rules an object can check by itself (title not empty, Id greater than zero) live in the **entity constructor and methods**.
- Rules that need the database (does this project exist?) live in the **service**, checked before the insert, so the client gets a clear `400` instead of a `500`.
- The **foreign key** stays in SQL as the final guarantee. Between the service check and the insert, another request could delete the project; only the constraint catches that.

**Async all the way down.**
Controller, service and repository are all async, down to Dapper's async methods. This frees the request thread while the database works, so the server handles more requests at once. Repositories await inside their `using` blocks, so the connection is never disposed before the query finishes.

**Every layer depends on interfaces.**
Controllers depend on `ITaskService`, services on repository interfaces, repositories on `ISqlConnectionFactory`. Repository interfaces live in **Domain**, so Infrastructure depends on Domain rather than the reverse. `ITaskService` lives in **Application**, because its methods use Application's DTOs. The main benefit is testability: any layer can be tested with a fake of the layer below.

**DTOs instead of exposing entities.**
The API accepts `CreateTaskRequest`, `UpdateTaskRequest` and `MoveTaskRequest`, and returns `TaskResponse`. Fields the system owns, such as `Id` and `CreatedAt`, cannot be sent by a client.

**Encapsulated entities.**
Entity properties have private setters and change only through methods such as `UpdateTitle`, `AssignTo` and `ChangeStatus`, which enforce their own rules. A private parameterless constructor lets Dapper create objects from database rows without bypassing those rules for application code.

**Centralised error handling.**
`ExceptionHandlingMiddleware` runs first in the pipeline. It maps `ArgumentException` and `InvalidOperationException` to `400`, and anything else to `500` with a generic message. The full exception is logged on the server, and never sent to the client.

**The database lives in the repository.**
Every table, foreign key and stored procedure is a script in `Database/`, so the database can be rebuilt from a fresh clone. The seed script and procedures are idempotent: re-running them causes no errors and no duplicate data.

---

## Limitations and next steps

TaskFlow is a work in progress. Not yet built:

- **Authentication**: JWT authentication and roles are planned.
- **Automated tests**: no unit or integration tests yet. The interface-based design is ready for them.
- **List endpoints**: the repository supports fetching tasks by project, but there is no endpoint for it yet. There are no endpoints for projects or users.
- **Extensible error mapping**: adding a new exception type currently means editing the middleware. A custom exception that carries its own status code would remove that.
- **Pagination and filtering** for list queries.
- **Frontend**: an Angular frontend is planned.
- **CI/CD**: no build pipeline yet.

---

## Author

**Ramees P.K.** · [github.com/Rameespk424](https://github.com/Rameespk424) · [pkramees.in](https://pkramees.in)