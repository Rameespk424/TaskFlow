using System;
using TaskFlow.Domain;


var task = new TaskItem("Fix login bug", 1);

task.AssignTo(5);
task.UpdateTitle("User Name Miss match");
task.SetDueDate(DateTime.UtcNow.AddDays(7));
task.ChangeStatus(TaskItemStatus.InProgress);

Console.WriteLine($"Id: {task.AssigneeId}");
Console.WriteLine($"Task: {task.Title}");
Console.WriteLine($"Due Date: {task.DueDate}");
Console.WriteLine($"Status: {task.Status}");
