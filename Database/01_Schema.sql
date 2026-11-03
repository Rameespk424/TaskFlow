/*
    TaskFlow - Schema
    Run order: 01_Schema.sql -> 02_ForeignKeys.sql -> StoredProcedures/*.sql

    Table order matters: Users and Projects must exist before Tasks,
    because Tasks references both.
*/

USE TaskFlow;
GO

CREATE TABLE Users (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Name      NVARCHAR(200) NOT NULL,
    Email     NVARCHAR(500) NOT NULL,
    CreatedAt DATETIME2     NOT NULL
);
GO

CREATE TABLE Projects (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Name        NVARCHAR(200)  NOT NULL,
    Description NVARCHAR(1000) NULL,
    OwnerId     INT            NOT NULL,
    CreatedAt   DATETIME2      NOT NULL
);
GO

CREATE TABLE Tasks (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Title       NVARCHAR(200)  NOT NULL,
    Description NVARCHAR(1000) NULL,
    Status      INT            NOT NULL,   -- maps to TaskItemStatus enum
    Priority    INT            NOT NULL,   -- maps to TaskPriority enum
    DueDate     DATETIME2      NULL,
    AssigneeId  INT            NULL,       -- NULL = unassigned
    ProjectId   INT            NOT NULL,
    CreatedAt   DATETIME2      NOT NULL
);
GO
