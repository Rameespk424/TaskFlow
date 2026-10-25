
USE TaskFlow
GO


DECLARE @Email NVARCHAR(500) = 'seed@mail.com';
DECLARE @ProjectName NVARCHAR(200) = 'SEEDED Project';

IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = @Email)

BEGIN
INSERT INTO Users(
Name, Email, CreatedAt
)
Values(
'SEEDED', @Email, GETUTCDATE()
);
END

DECLARE @UserId INT = (SELECT Id FROM Users WHERE Email = @Email);

IF NOT EXISTS (SELECT 1 FROM Projects WHERE NAME = @ProjectName )

BEGIN
INSERT INTO Projects(
Name, Description, OwnerId, CreatedAt
)
VALUES(
@ProjectName, 'Test Desc',  @UserId, GETUTCDATE()
);
END

```