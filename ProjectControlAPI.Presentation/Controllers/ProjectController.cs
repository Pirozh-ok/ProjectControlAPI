using Microsoft.AspNetCore.Mvc;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs.ProjectDTOs;

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

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await _projectService.GetAllAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDTO project)
        {
            await _projectService.UpdateAsync(project);
            return StatusCode(204);
        }
    }
}
