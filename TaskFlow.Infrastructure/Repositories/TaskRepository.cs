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


        public IEnumerable<TaskItem> GetByProjectId(int projectId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return connection.Query<TaskItem>(
                "usp_GetTaskByProject",
                new { ProjectId = projectId },
                commandType: CommandType.StoredProcedure);
        }

        public int Add(TaskItem task)
        {
            using var connection = _connectionFactory.CreateConnection();

            return connection.QuerySingle<int>(

                "usp_CreateTask",
                new
                {
                    task.Title,
                    task.Description,
                    task.Status,
                    task.Priority,
                    task.DueDate,
                    task.AssigneeId,
                    task.ProjectId,
                    task.CreatedAt,
                }, commandType: CommandType.StoredProcedure

                );

        }


        public bool Update(TaskItem task)
        {
           using  var connection = _connectionFactory.CreateConnection();       
            
            int rowAffected = connection.QuerySingle<int>(

                "usp_UpdateTask",
                new
                {
                    task.Id,
                    task.Title,
                    task.Description,
                    task.Status,
                    task.Priority,
                    task.DueDate,
                    task.AssigneeId,
                    task.ProjectId,
                }, commandType: CommandType.StoredProcedure);
            return rowAffected > 0;
        }

        public bool Delete(int id) {

            using var connection = _connectionFactory.CreateConnection();

            int rowDeleted = connection.QuerySingle<int>(
                "usp_DeleteTask",
                new {Id = id},
                commandType : CommandType.StoredProcedure);

            return rowDeleted > 0;

        }

    }
}
