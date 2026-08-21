using System;
using TaskFlow.Domain;


var task = new TaskItem("Fix login bug", 1);

var user = new User("Ramees Pk", "newemail@example.com");

var project = new Project("New Project", 1);

task.AssignTo(5);
task.UpdateTitle("User Name Miss match");
task.SetDueDate(DateTime.UtcNow.AddDays(7));
task.ChangeStatus(TaskItemStatus.Done);
task.ReOpenTask();



Console.WriteLine($"Id: {task.AssigneeId}");
Console.WriteLine($"Task: {task.Title}");
Console.WriteLine($"Due Date: {task.DueDate}");
Console.WriteLine($"Status: {task.Status}");
Console.WriteLine($"New User: {user.Name} | {user.Email}");
Console.WriteLine($"New Project Name: {project.Name} |  {project.OwnerId}");
