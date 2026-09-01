using TaskFlow.Domain;
using TaskFlow.Domain.Repositories;
using TaskFlow.Infrastructure.Persistence;
using TaskFlow.Infrastructure.Repositories;


var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TaskFlow;Trusted_Connection=True;TrustServerCertificate=True;";
ISqlConnectionFactory factory = new SqlConnectionFactory(connectionString);
ITaskRepository repository = new TaskRepository(factory);

TaskItem? task = repository.GetById(1);

if(task is null)
{
    Console.WriteLine("NO Task was founf");
} else
{
    Console.WriteLine($"Id:{task.Id}");
    Console.WriteLine($"Title:{task.Title}");
    Console.WriteLine($"Description:{task.Description}");
}