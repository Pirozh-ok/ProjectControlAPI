using ProjectControlAPI.Common.DTOs.ProjectDTOs;
using ProjectControlAPI.Common.DTOs.TaskDTOs;
using ProjectControlAPI.Common.DTOs.WorkerDTOs;
using ProjectControlAPI.Common.QueryParameters;

namespace ProjectControlAPI.BusinessLogic.Services.Implementations
{
    public interface IProjectService
    {
        Task CreateAsync(CreateProjectDTO project);
        Task DeleteAsync(int projectId);
        Task UpdateAsync(UpdateProjectDTO project);
        Task<GetProjectDTO> GetByIdAsync(int projectId);
        Task<IEnumerable<GetProjectDTO>> GetAllAsync(ProjectsParameters projectParameters);
        Task<IEnumerable<GetWorkerDTO>> GetWorkersByProjectAsync(int projectId);
        Task<IEnumerable<GetTaskDTO>> GetTasksByProjectAsync(int projectId);
        Task AddWorkerOnProject(int projectId, int workerId);
        Task RemoveWorkerFromProject(int projectId, int workerId);
    }
}
