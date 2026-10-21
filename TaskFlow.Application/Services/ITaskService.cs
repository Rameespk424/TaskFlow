using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Services
{
    public interface ITaskService
    {
        Task<int> CreateTaskAsync(CreateTaskRequest request);
        Task<TaskResponse?> GetTaskByIdAsync(int id);
        Task<bool> DeleteTaskAsync(int id);
        Task<bool> UpdateTaskAsync(int id, UpdateTaskRequest request);
        Task<bool> MoveToProjectAsync(int id, MoveTaskRequest request);

    }
}
