
namespace TaskFlow.Domain.Repositories
{
    public interface IProjectRepository
    {

        Task<bool> ExistsAsync(int projectId);

    }
}
