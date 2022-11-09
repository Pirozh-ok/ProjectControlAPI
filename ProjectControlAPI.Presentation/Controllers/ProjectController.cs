using Microsoft.AspNetCore.Mvc;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs.ProjectDTOs;
using ProjectControlAPI.Common.QueryParameters;

namespace ProjectControlAPI.Presentation.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDTO project)
        {
            await _projectService.CreateAsync(project);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectService.DeleteAsync(id);
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            return Ok(await _projectService.GetByIdAsync(id));
        }

        [HttpGet("{id}/workers")]
        public async Task<IActionResult> GetWorkersByProject(int id)
        {
            return Ok(await _projectService.GetWorkersByProjectAsync(id));
        }

        [HttpGet("{id}/tasks")]
        public async Task<IActionResult> GetTasksByProject(int id)
        {
            return Ok(await _projectService.GetTasksByProjectAsync(id)); 
        }

        [HttpPost("{id}/add-worker")]
        public async Task<IActionResult> AddWorkerToProject(int id, [FromQuery] int workerId)
        {
            await _projectService.AddWorkerOnProject(id, workerId);
            return StatusCode(201);
        }

        [HttpDelete("{id}/remove-worker")]
        public async Task<IActionResult> RemoveWorkerToProject(int id, [FromQuery] int workerId)
        {
            await _projectService.RemoveWorkerFromProject(id, workerId);
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] ProjectsParameters projectsParameters)
        {
            return Ok(await _projectService.GetAllAsync(projectsParameters));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDTO project)
        {
            await _projectService.UpdateAsync(project);
            return StatusCode(204);
        }
    }
}
