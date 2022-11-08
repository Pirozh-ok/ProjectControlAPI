using Microsoft.AspNetCore.Mvc;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs.TaskDTOs;

namespace ProjectControlAPI.Presentation.Controllers
{
    [Route("api/tasks/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDTO task)
        {
            await _taskService.CreateAsync(task);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteAsync(id);
            return StatusCode(204);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            return Ok(await _taskService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            return Ok(await _taskService.GetAllAsync());
        }

        //[HttpPut]
        //public async 
    }
}
