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

        public async Task<TaskItem?> GetByIdAsync(int id)
        {

            using var connection = _connectionFactory.CreateConnection();

          return await connection.QuerySingleAsync<TaskItem>(
                "usp_GetTaskById",
                    new { Id = id},
                    commandType: CommandType.StoredProcedure);

        }


        public IEnumerable<TaskItem> GetByProjectIdAsync(int projectId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return connection.Query<TaskItem>(
                "usp_GetTaskByProject",
                new { ProjectId = projectId },
                commandType: CommandType.StoredProcedure);
        }

        public async  Task<int> AddAsync(TaskItem task)
        {
            using var connection =  _connectionFactory.CreateConnection();

            return await connection.QuerySingleAsync<int>(

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


        public async Task<bool> UpdateAsync(TaskItem task)
        {
           using  var connection = _connectionFactory.CreateConnection();       
            
            int rowAffected = await connection.QuerySingleAsync<int>(

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
            return   rowAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id) {

            using var  connection = _connectionFactory.CreateConnection();

            int rowDeleted = await connection.QuerySingleAsync<int>(
                "usp_DeleteTask",
                new {Id = id},
                commandType : CommandType.StoredProcedure);

            return rowDeleted > 0;

        }

    }
}
