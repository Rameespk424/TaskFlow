/*
    Inserts a task and returns the generated Id.

    SCOPE_IDENTITY() is used rather than @@IDENTITY so that a future trigger
    on Tasks cannot cause the wrong Id to be returned.
    The CAST is needed because SCOPE_IDENTITY() returns NUMERIC(38,0).
*/

CREATE OR ALTER PROCEDURE usp_CreateTask
    @Title       NVARCHAR(200),
    @Description NVARCHAR(1000),
    @Status      INT,
    @Priority    INT,
    @DueDate     DATETIME2,
    @AssigneeId  INT,
    @ProjectId   INT,
    @CreatedAt   DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tasks (Title, Description, Status, Priority,
                       DueDate, AssigneeId, ProjectId, CreatedAt)
    VALUES (@Title,
            @Description,
            @Status,
            @Priority,
            @DueDate,
            @AssigneeId,
            @ProjectId,
            @CreatedAt);

    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO
