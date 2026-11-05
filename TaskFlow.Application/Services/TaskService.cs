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
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<int> CreateTaskAsync(CreateTaskRequest request)
        {
            bool projectExist = await _projectRepository.ExistsAsync(request.ProjectId);
            if (!projectExist)
            {
                throw new ArgumentException($"Project {request.ProjectId} does not exist");
            }

            var taskItem = new TaskItem(request.Title, request.ProjectId);

            taskItem.SetPriority((TaskPriority)request.Priority);
            taskItem.UpdateDescription(request.Description);
            taskItem.AssignTo(request.AssigneeId);
            return await _taskRepository.AddAsync(taskItem);

        }

        public async Task<TaskResponse?> GetTaskByIdAsync(int id)
        {

            var taskItem = await _taskRepository.GetByIdAsync(id);

            if (taskItem is null)
            {
                return null;
            }
            else
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
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var  taskItem = await _taskRepository.DeleteAsync(id);

            return  taskItem;
        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskRequest request)
        {

            var task = await _taskRepository.GetByIdAsync(id);
            if (task is null)
            {
                return false;
            }


            task.UpdateTitle(request.Title);
            task.UpdateDescription(request.Description);
            task.SetPriority((TaskPriority)request.Priority);
            task.ChangeStatus((TaskItemStatus)request.Status);
            if (request.AssigneeId.HasValue)
            {
                task.AssignTo(request.AssigneeId.Value);

            }

            return await _taskRepository.UpdateAsync(task);

        }

        public async Task<bool> MoveToProjectAsync(int id, MoveTaskRequest request)
        {
            var taskItem = await _taskRepository.GetByIdAsync(id);
            if (taskItem is null)
                return false;

            bool projectExists = await _projectRepository.ExistsAsync(request.ProjectId);

            if (!projectExists)
            {
                throw new ArgumentException($"Project {request.ProjectId} does not exist");
            }


            taskItem.MoveToNewProject(request.ProjectId);

            return await _taskRepository.UpdateAsync(taskItem);
        }
    }
}
