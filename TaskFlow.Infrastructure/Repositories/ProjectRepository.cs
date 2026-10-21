
using Dapper;
using TaskFlow.Domain.Repositories;
using TaskFlow.Infrastructure.Persistence;
using System.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public ProjectRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> ExistsAsync(int projectId)
        {
            using var connection = _connectionFactory.CreateConnection();

            int projectExist = await connection.QuerySingleAsync<int>(

                    "usp_ProjectExists",
                    new { Id = projectId },
                    commandType: CommandType.StoredProcedure
                );
            return projectExist > 0;
                 
        }
    }
}
