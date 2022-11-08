using ProjectControlAPI.Common.DTOs.TaskDTOs;

namespace ProjectControlAPI.BusinessLogic.Services.Implementations
{
    public interface ITaskService
    {
        Task CreateAsync(CreateTaskDTO task);
        Task<GetTaskDTO> GetByIdAsync(int taskId);
        Task<IEnumerable<GetTaskDTO>> GetAllAsync();
        Task DeleteAsync(int taskId);
        Task UpdateAsync(UpdateTaskDTO task);
    }
}
