/*
    Deletes a task and returns the number of rows affected.

    Deleting a non-existent Id is not an error in SQL Server - it simply
    affects zero rows. @@ROWCOUNT is what lets the caller tell the
    difference between "deleted" and "was never there".
*/

CREATE OR ALTER PROCEDURE usp_DeleteTask
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Tasks
    WHERE Id = @Id;

    SELECT @@ROWCOUNT;
END
GO
