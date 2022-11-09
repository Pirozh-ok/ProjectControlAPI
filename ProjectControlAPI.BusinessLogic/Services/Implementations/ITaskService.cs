using ProjectControlAPI.Common.DTOs.TaskDTOs;
using ProjectControlAPI.Common.QueryParameters;

namespace ProjectControlAPI.BusinessLogic.Services.Implementations
{
    public interface ITaskService
    {
        Task CreateAsync(CreateTaskDTO task);
        Task<GetTaskDTO> GetByIdAsync(int taskId);
        Task<IEnumerable<GetTaskDTO>> GetAllAsync(TaskParameters parameters);
        Task DeleteAsync(int taskId);
        Task UpdateAsync(UpdateTaskDTO task);
        Task UpdateStatusAsync(UpdateStatusTaskDTO task);
    }
}
