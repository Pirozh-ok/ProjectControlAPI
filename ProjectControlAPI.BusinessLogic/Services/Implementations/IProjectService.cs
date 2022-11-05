using ProjectControlAPI.Common.DTOs.ProjectDTOs;

namespace ProjectControlAPI.BusinessLogic.Services.Implementations
{
    public interface IProjectService
    {
        Task CreateAsync(CreateProjectDTO project);
        Task DeleteAsync(int projectId);
        Task UpdateAsync(UpdateProjectDTO project);
        Task<GetProjectDTO> GetByIdAsync(int projectId);
        Task<IEnumerable<GetProjectDTO>> GetAllAsync();
    }
}
