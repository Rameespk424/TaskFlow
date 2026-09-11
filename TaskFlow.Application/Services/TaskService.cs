using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain;
using TaskFlow.Domain.Repositories;

namespace TaskFlow.Application.Services
{
    public class TaskService
    {

        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public int CreateTask(CreateTaskRequest request)
        {

            var task = new TaskItem(request.Title, request.ProjectId);

            task.SetPriority((TaskPriority)request.Priority);
            task.UpdateDescription(request.Description);
            task.AssignTo(request.AssignTo);
            return _taskRepository.Add(task);

        }


        public TaskResponse? GetTaskById(int id)
        {

            var taskItem = _taskRepository.GetById(id);

            if (taskItem is null ) {
            return null;
            } else
            {
                var response = new TaskResponse();

                response.Id = taskItem.Id;
                response.Title = taskItem.Title;
                response.Description = taskItem.Description;
                response.DueDate = taskItem.DueDate;
                response.Status = taskItem.Status.ToString();
                response.Priority = taskItem.Priority.ToString();
                response.ProjectId = taskItem.ProjectId;
                response.AssigneeId = taskItem.AssigneeId;

                return response;
            }
        }
        public bool DeleteTask(int Id)
        {
            var taskItem = _taskRepository.Delete(Id);

            return taskItem;
        }

        public bool UpdateTask(int Id, UpdateTaskRequest request )
        {

            var task = _taskRepository.GetById(Id);
            if (task is null)
            {
                return false;
            }

            bool updatedtask = _taskRepository.Update(task);

            task.UpdateTitle(request.Title);
            task.UpdateDescription(request.Description);
            task.SetPriority((TaskPriority)request.Priority);
            task.ChangeStatus((TaskItemStatus)request.Status);
            if(request.AssigneeId.HasValue)
            {
                task.AssignTo(request.AssigneeId.Value);

            }

            return _taskRepository.Update(task);

        }


        public bool MoveToProject(int id , int newProjectId)
        {
            var task = _taskRepository.GetById(id);

            if (task is null)
                return false;

            task.MoveToNewProject(newProjectId);

            return _taskRepository.Update(task);
        }
    }
}
