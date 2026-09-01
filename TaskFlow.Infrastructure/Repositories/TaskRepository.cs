using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain;
using TaskFlow.Domain.Repositories;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {

       private readonly ISqlConnectionFactory _connectionFactory;

        public TaskRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public TaskItem? GetById(int Id)
        {

            using var connnection = _connectionFactory.CreateConnection();

            return connnection.QuerySingleOrDefault<TaskItem>(
                "usp_GetTaskById",
                    new { Id = Id},
                    commandType: CommandType.StoredProcedure);

            }


    }
}
