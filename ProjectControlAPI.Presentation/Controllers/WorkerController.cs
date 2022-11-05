using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkerById(int id)
        {
            return Ok(await _workerService.GetByIdAsync(id)); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            await _workerService.DeleteAsync(id);
            return StatusCode(204); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorker([FromBody] CreateWorkerDTO worker)
        {
            await _workerService.CreateAsync(worker);
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWorker([FromBody] UpdateWorkerDTO worker)
        {
            await _workerService.UpdateAsync(worker);
            return StatusCode(204);
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkers()
        {
            return Ok(await _workerService.GetAllAsync());
        }
    }
}
