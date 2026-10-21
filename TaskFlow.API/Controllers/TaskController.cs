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
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskRequest request)
        {
            int newId =await _taskService.CreateTaskAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = newId },  new { id = newId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) { 

            var taskResponse = await  _taskService.GetTaskByIdAsync(id);

            if (taskResponse is null)
                return NotFound();

            return Ok(taskResponse);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            bool delete = await _taskService.DeleteTaskAsync(id);

            if (!delete)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, UpdateTaskRequest request)
        {
            bool changedTask = await _taskService.UpdateTaskAsync(id, request);

            if (!changedTask)
                return NotFound();

            return NoContent();


        }


        [HttpPut("{id}/project")]

        public async Task<IActionResult> MoveToProject(int id, MoveTaskRequest request)
        {
            var movedTask = await _taskService.MoveToProjectAsync(id, request);

                if(!movedTask)
                return NotFound();
            return NoContent();
        }

    }
}
