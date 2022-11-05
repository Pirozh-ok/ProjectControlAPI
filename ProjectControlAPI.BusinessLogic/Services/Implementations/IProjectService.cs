using ProjectControlAPI.Common.DTOs.ProjectDTOs;
using ProjectControlAPI.Common.DTOs.WorkerDTOs;

namespace ProjectControlAPI.BusinessLogic.Services.Implementations
{
    public interface IProjectService
    {
        Task CreateAsync(CreateProjectDTO project);
        Task DeleteAsync(int projectId);
        Task UpdateAsync(UpdateProjectDTO project);
        Task<GetProjectDTO> GetByIdAsync(int projectId);
        Task<IEnumerable<GetProjectDTO>> GetAllAsync();
        Task<IEnumerable<GetWorkerDTO>> GetWorkersByProject(int projectId);
        Task AddWorkerOnProject(int projectId, int workerId);
        Task RemoveWorkerFromProject(int projectId, int workerId);
    }
}
