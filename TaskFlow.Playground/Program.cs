using System;
using TaskFlow.Domain;


var task = new TaskItem("New Task", 1);

Console.WriteLine($"{task.Title} | {task.Status} | {task.CreatedAt}");