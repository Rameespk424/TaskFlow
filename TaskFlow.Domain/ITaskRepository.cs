

namespace TaskFlow.Domain
{
    public interface ITaskRepository
    {

         TaskItem? GetById(int id);
        List<TaskItem> GetByProjectId(int projectId);
        void Add(TaskItem task);
        void Update(TaskItem task);

    }
}
