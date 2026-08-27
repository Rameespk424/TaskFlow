/*
    Updates an existing task and returns the number of rows affected.

    CreatedAt is deliberately not updatable - it is a creation fact.

    @@ROWCOUNT must be read immediately after the UPDATE. Any statement in
    between resets it, including a SELECT.
    Returns 0 when no task matches @Id, which lets the API answer 404
    instead of reporting a false success.
*/

CREATE OR ALTER PROCEDURE usp_UpdateTask
    @Id          INT,
    @Title       NVARCHAR(200),
    @Description NVARCHAR(1000),
    @Status      INT,
    @Priority    INT,
    @DueDate     DATETIME2,
    @AssigneeId  INT,
    @ProjectId   INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Tasks
    SET Title       = @Title,
        Description = @Description,
        Status      = @Status,
        Priority    = @Priority,
        DueDate     = @DueDate,
        AssigneeId  = @AssigneeId,
        ProjectId   = @ProjectId
    WHERE Id = @Id;

    SELECT @@ROWCOUNT;
END
GO
