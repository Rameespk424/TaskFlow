/*
    Returns all tasks belonging to a project.

    Returns zero rows for a project with no tasks - that is a valid result,
    not an error.
*/

CREATE OR ALTER PROCEDURE usp_GetTaskByProject
    @ProjectId INT
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
    WHERE ProjectId = @ProjectId;
END
GO
