using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs.WorkerDTOs;

namespace ProjectControlAPI.Presentation.Controllers
{
    [Route("api/workers/")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [Authorize(Policy = "DirectorOrPM")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkerById(int id)
        {
            return Ok(await _workerService.GetByIdAsync(id)); 
        }

        [Authorize(Policy = "DirectorOrPM")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            await _workerService.DeleteAsync(id);
            return StatusCode(204); 
        }

        [Authorize(Policy = "DirectorOrPM")]
        [HttpPost]
        public async Task<IActionResult> CreateWorker([FromBody] CreateWorkerDTO worker)
        {
            await _workerService.CreateAsync(worker);
            return StatusCode(201);
        }

        [Authorize(Policy = "DirectorOrPM")]
        [HttpPut]
        public async Task<IActionResult> UpdateWorker([FromBody] UpdateWorkerDTO worker)
        {
            await _workerService.UpdateAsync(worker);
            return StatusCode(204);
        }

        [Authorize(Policy = "DirectorOrPM")]
        [HttpGet]
        public async Task<IActionResult> GetWorkers()
        {
            return Ok(await _workerService.GetAllAsync());
        }

        [Authorize(Policy = "DirectorOrPM")]
        [Authorize]
        [HttpGet("{id}/projects")]
        public async Task<IActionResult> GetWorkerProjects(int id)
        {
            return Ok(await _workerService.GetProjectsByWorkerAsync(id));
        }

        [Authorize(Policy = "AllWorkers")]
        [HttpGet("{id}/my-projects")]
        public async Task<IActionResult> GetMyProjects()
        {
            var claim = HttpContext.User.Claims.First(c => c.Type == "NameIdentifier");
            var id = int.Parse(claim.Value);
            return Ok(await _workerService.GetProjectsByWorkerAsync(id));
        }

        [Authorize(Policy = "AllWorkers")]
        [HttpGet("{id}/perfome-tasks")]
        public async Task<IActionResult> GetPerfomeTasksByWorker(int id)
        {
            return Ok(await _workerService.GetPerfomeTasksByWorkerAsync(id));
        }

        [Authorize(Policy = "DirectorOrPM")]
        [HttpGet("{id}/created-tasks")]
        public async Task<IActionResult> GetCreatedTasksByWorker(int id)
        {
            return Ok(await _workerService.GetCreatedTasksByWorkerAsync(id));
        }
    }
}
