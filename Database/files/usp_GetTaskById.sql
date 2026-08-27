/*
    Returns a single task by Id.

    Columns are listed explicitly rather than using SELECT *, so adding a
    column later does not silently change what every caller receives.
    Column names match the TaskItem entity property names so Dapper maps
    them without manual configuration.
*/

CREATE OR ALTER PROCEDURE usp_GetTaskById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id,
           Title,
           Description,
           Status,
           Priority,
           DueDate,
           AssigneeId,
           ProjectId,
           CreatedAt
    FROM Tasks
    WHERE Id = @Id;
END
GO
