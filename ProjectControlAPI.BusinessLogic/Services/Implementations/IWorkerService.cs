using ProjectControlAPI.Common.DTOs;

namespace ProjectControlAPI.BusinessLogic.Services.Implementations
{
    public interface IWorkerService
    {
        Task CreateAsync(CreateWorkerDTO worker);
        Task UpdateAsync(UpdateWorkerDTO worker);
        Task DeleteAsync(int workerId);
        Task<GetWorkerDTO> GetByIdAsync(int workerId);
        Task<IEnumerable<GetWorkerDTO>> GetAllAsync(); 
    }
}
