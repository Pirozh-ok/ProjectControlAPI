using ProjectControlAPI.Common.DTOs;
using ProjectControlAPI.Common.DTOs.ProjectDTOs;
using ProjectControlAPI.Common.DTOs.TaskDTOs;
using ProjectControlAPI.Common.DTOs.WorkerDTOs;

namespace ProjectControlAPI.BusinessLogic.Services.Implementations
{
    public interface IWorkerService
    {
        Task CreateAsync(CreateWorkerDTO worker);
        Task UpdateAsync(UpdateWorkerDTO worker);
        Task DeleteAsync(int workerId);
        Task<GetWorkerDTO> GetByIdAsync(int workerId);
        Task<IEnumerable<GetWorkerDTO>> GetAllAsync();
        Task<IEnumerable<GetProjectDTO>> GetProjectsByWorkerAsync(int workerId);
        Task<IEnumerable<GetTaskDTO>> GetCreatedTasksByWorkerAsync(int workerId);
        Task<IEnumerable<GetTaskDTO>> GetPerfomeTasksByWorkerAsync(int workerId);
        Task<AuthResponseDTO> SignInAsync(string mail);
    }
}
