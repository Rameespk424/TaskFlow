using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain
{
    public class TaskItem
    {

        public int Id { get; private set; }
        public string Title { get; private set; } 
        public string? Description { get; set; }
        public TaskStatus Status { get; private set; } = TaskStatus.Todo;
        public TaskPriority Priority { get; private set; } = TaskPriority.Medium;
        private DateTime? DueDate { get; set; }
        private int? AssigneeId { get; set; } 
        public int ProjectId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public TaskItem(string title, int projectId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is required", nameof(title));
            }

            if (projectId <= 0) {
            throw new ArgumentException("ProjectId must be a positive integer", nameof(projectId));
            }


            Title = title;
            ProjectId = projectId;
            CreatedAt = DateTime.UtcNow;
        }

    }


}
