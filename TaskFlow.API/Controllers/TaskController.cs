using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Services;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain;


namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public IActionResult Create(CreateTaskRequest request)
        {
            int newId =_taskService.CreateTask(request);
            return Ok(new { id = newId });
        }

        [HttpGet]
        public IActionResult GetNewTaskt(int id) { 

            var task = _taskService.GetTaskById(id);

            if (task is null)
                return NotFound();

            return Ok(task);

        }

        [HttpDelete]

        public IActionResult DeleteTaskItem(int id)
        {
            bool delete = _taskService.DeleteTask(id);

            if (!delete)
                return NotFound();

            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateTaskItem(int id, UpdateTaskRequest request)
        {
            bool changedTask = _taskService.UpdateTask(id, request);

            if (!changedTask)
                return NotFound();

            return NoContent();


        }
      

    }
}
