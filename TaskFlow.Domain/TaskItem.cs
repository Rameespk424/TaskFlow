using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain
{
    public class TaskItem : BaseEntity
    {

        public string Title { get; private set; }
        public string? Description { get; set; }
        public TaskItemStatus Status { get; private set; } = TaskItemStatus.Todo;
        public TaskPriority Priority { get; private set; } = TaskPriority.Medium;
        public DateTime? DueDate { get; private set; }
        public int? AssigneeId { get; private set; }
        public int ProjectId { get; private set; }

        public TaskItem(string title, int projectId) : base()
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is required", nameof(title));
            }

            if (projectId <= 0)
            {
                throw new ArgumentException("ProjectId must be a positive integer", nameof(projectId));
            }


            Title = title;
            ProjectId = projectId;
        }



        public void AssignTo(int userId)
        {

            if(userId <= 0)
            {
                throw new ArgumentException("UserId must be a positive integer", nameof(userId));
            }

            AssigneeId = userId;

        }

        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentException("Title is required", nameof(newTitle));
            }

            Title = newTitle;
        }


        public void SetDueDate(DateTime dueDate)
        {
            if( dueDate < CreatedAt)
            {
                throw new ArgumentException("DueDate must be a future date", nameof(dueDate));
            }
            
            DueDate = dueDate;

        }

        public void ChangeStatus(TaskItemStatus newStatus)
        {
            if(Status == TaskItemStatus.Done && newStatus == TaskItemStatus.Todo )
            {
                throw new InvalidOperationException("Cannot move a completed task back to Todo");
            }

            Status = newStatus;
        }

        public void ReOpenTask()
        {
            if(Status != TaskItemStatus.Done)
            {
                throw new InvalidOperationException("Only completed tasks can be reopened");
            }

            Status = TaskItemStatus.Todo;

        }

    }

}