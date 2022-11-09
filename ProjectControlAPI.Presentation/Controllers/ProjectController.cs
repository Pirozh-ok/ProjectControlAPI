using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs.ProjectDTOs;
using ProjectControlAPI.Common.QueryParameters;

namespace ProjectControlAPI.Presentation.Controllers
{
    [Route("api/projects/")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize(Policy = "Director")]
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDTO project)
        {
            await _projectService.CreateAsync(project);
            return StatusCode(201);
        }

        [Authorize(Policy = "Director")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectService.DeleteAsync(id);
            return StatusCode(201);
        }

        [Authorize(Policy = "Director")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            return Ok(await _projectService.GetByIdAsync(id));
        }

        [Authorize(Policy = "DirectorOrPM")]
        [HttpGet("{id}/workers")]
        public async Task<IActionResult> GetWorkersByProject(int id)
        {
            return Ok(await _projectService.GetWorkersByProjectAsync(id));
        }

        [Authorize(Policy = "DirectorOrPM")]
        [HttpGet("{id}/tasks")]
        public async Task<IActionResult> GetTasksByProject(int id)
        {
            return Ok(await _projectService.GetTasksByProjectAsync(id)); 
        }

        [Authorize(Policy = "DirectorOrPM")]
        [HttpPost("{id}/add-worker")]
        public async Task<IActionResult> AddWorkerToProject(int id, [FromQuery] int workerId)
        {
            await _projectService.AddWorkerOnProject(id, workerId);
            return StatusCode(201);
        }

        [Authorize(Policy = "DirectorOrPM")]
        [HttpDelete("{id}/remove-worker")]
        public async Task<IActionResult> RemoveWorkerToProject(int id, [FromQuery] int workerId)
        {
            await _projectService.RemoveWorkerFromProject(id, workerId);
            return StatusCode(201);
        }

        [Authorize(Policy = "Director")]
        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] ProjectsParameters projectsParameters)
        {
            return Ok(await _projectService.GetAllAsync(projectsParameters));
        }

        [Authorize(Policy = "AllWorker")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyProjects([FromQuery] ProjectsParameters projectsParameters)
        {
            return Ok(await _projectService.GetMyProjectsAsync(10, projectsParameters));
        }

        [Authorize(Policy = "Director")]
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDTO project)
        {
            await _projectService.UpdateAsync(project);
            return StatusCode(204);
        }
    }
}
