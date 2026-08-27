/*
    TaskFlow - Foreign keys
    Run after 01_Schema.sql

    Naming convention: FK_ChildTable_ParentTable
    A named constraint tells you which relationship failed when it shows
    up in a production error log.

    All three use the default ON DELETE NO ACTION:
      - Tasks.ProjectId  is NOT NULL, so a task cannot outlive its project.
                         Deleting a project with tasks is blocked deliberately.
      - Tasks.AssigneeId is NULL-able. NULL means no relationship and the
                         constraint is not checked for that row.
*/

ALTER TABLE Projects
ADD CONSTRAINT FK_Projects_Users
FOREIGN KEY (OwnerId) REFERENCES Users(Id);
GO

ALTER TABLE Tasks
ADD CONSTRAINT FK_Tasks_Projects
FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
GO

ALTER TABLE Tasks
ADD CONSTRAINT FK_Tasks_Users
FOREIGN KEY (AssigneeId) REFERENCES Users(Id);
GO
