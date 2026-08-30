using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain
{
    public class InMemoryTaskRepository : ITaskRepository  
    {

        private readonly List<TaskItem> _task = new();
        public void Add(TaskItem task)
        {
            _task.Add(task);
        }

        public TaskItem? GetById(int id)
        {
            return _task.FirstOrDefault(t => t.Id == id);
        }

        public List<TaskItem> GetByProjectId(int projectId)
        {
            return _task.Where(t => t.ProjectId == projectId).ToList();
        }

        public void Update(TaskItem task)
        {
            var index = _task.FindIndex(t => t.Id == task.Id);

            if(index >= 0)
            {
                _task[index] = task;
            }
        }

    }   
}
