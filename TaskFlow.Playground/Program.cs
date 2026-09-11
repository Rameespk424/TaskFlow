using TaskFlow.Domain;
using TaskFlow.Domain.Repositories;
using TaskFlow.Infrastructure.Persistence;
using TaskFlow.Infrastructure.Repositories;
using System.Linq;


var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TaskFlow;Trusted_Connection=True;TrustServerCertificate=True;";
ISqlConnectionFactory factory = new SqlConnectionFactory(connectionString);
ITaskRepository repository = new TaskRepository(factory);



//TaskItem? task = repository.GetById(1);

//if(task is null)
//{
//    Console.WriteLine("NO Task was found");
//} else
//{
//    Console.WriteLine($"Id:{task.Id}");
//    Console.WriteLine($"Title:{task.Title}");
//    Console.WriteLine($"Description:{task.Description}");
//}

//IEnumerable<TaskItem> tasks = repository.GetByProjectId(1);




//if(!tasks.Any())
//{
//    Console.WriteLine("No Tasks was found");
//} else
//{
//    foreach (var items in tasks)
//    {
//        Console.WriteLine($"Id:{items.Id}");
//        Console.WriteLine($"Title:{items.Title}");
//        Console.WriteLine($"Description:{items.Description}");
//    }
//}

//var newtask = new TaskItem("test Task", 1);

//int newId = repository.Add(newtask);

//if(newId <= 0 )
//{
//    Console.WriteLine("failed");

//}
//else
//{
//    Console.WriteLine($"TaskID:{newId}");

//}





//var updatedTask = repository.GetById(1);

//if (updatedTask is not null)
//{
//    updatedTask.UpdateTitle("Updated Title");
//    updatedTask.ChangeStatus(TaskItemStatus.Done);

//    bool updated = repository.Update(updatedTask);
//    Console.WriteLine(updated ? "Task Updated" : "Task not updated");

//}


//var deletedTask = repository.GetById(15);

//if(deletedTask is not null)
//{
//    bool taskDeleted = repository.Delete(deletedTask);

//    Console.WriteLine($"deleted {taskDeleted}");
//        }
