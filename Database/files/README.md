# TaskFlow Database

SQL Server scripts for the TaskFlow database. Everything here is versioned
alongside the application code so the schema can be rebuilt from the repo
rather than copied from a developer machine.

## Rebuilding from scratch

```sql
CREATE DATABASE TaskFlow;
GO

USE TaskFlow;
GO
```

Then run, in this order:

1. `01_Schema.sql`
2. `02_ForeignKeys.sql`
3. Every file in `StoredProcedures/` (order does not matter)

The numbering matters for the first two: foreign keys cannot be added to
tables that do not exist yet.

## Notes

- Stored procedures use `CREATE OR ALTER`, so re-running a file updates the
  procedure instead of failing with "already exists".
- Column names match the entity property names in `TaskFlow.Domain` so Dapper
  maps results automatically.
- `Status` and `Priority` are stored as `INT` because the C# enums already
  have int underlying values. Storing the text would break on every rename.
- Timestamps are UTC. The application sets `CreatedAt` via
  `BaseEntity` (`DateTime.UtcNow`), so it is passed in as a parameter rather
  than defaulted in the database - one source of truth.
