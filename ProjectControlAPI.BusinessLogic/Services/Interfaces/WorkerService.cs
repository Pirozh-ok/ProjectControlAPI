using Microsoft.EntityFrameworkCore;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs;
using ProjectControlAPI.Common.Exceptions;
using ProjectControlAPI.DataAccess;

namespace ProjectControlAPI.BusinessLogic.Services.Interfaces
{
    public class WorkerService : IWorkerService
    {
        private readonly DataContext _context; 

        public WorkerService(DataContext context)
        {
            _context = context; 
        }

        public async Task CreateAsync(CreateWorkerDTO worker)
        {
            if (worker is null)
            {
                throw new BadRequestException("Null arguments");
            }

            await _context.Workers.AddAsync(
                new DataAccess.Entities.Worker
                {
                    FirstName = worker.FirstName,
                    LastName = worker.LastName,
                    Patronymic = worker.Patronymic,
                    Mail = worker.Mail,
                });

            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(int workerId)
        {
            var worker = await _context.Workers
                .SingleOrDefaultAsync(x => x.Id == workerId);

            if(worker is null)
            {
                throw new NotFoundException("Worker is not found"); 
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<GetWorkerDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetWorkerDTO> GetByIdAsync(int workerId)
        {
            var worker = await _context.Workers
                .SingleOrDefaultAsync(x => x.Id == workerId);

            if(worker is null)
            {
                throw new NotFoundException("Worker is not found");
            }

            var projectsId = await _context.WorkerProject
                    .Where(x => x.WorkerId == workerId)
                    .Select(x => x.ProjectId)
                    .ToListAsync();

            return new GetWorkerDTO
            {
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Patronymic = worker.Patronymic,
                Mail = worker.Mail,
                Projects = await _context.Projects
                        .Where(x => projectsId
                        .Contains(x.Id)).ToListAsync()
            };
        }

        public async Task UpdateAsync(UpdateWorkerDTO worker)
        {
            if(worker is null)
            {
                throw new BadRequestException("Arguments is null"); 
            }

            var workerToUpdate = await _context.Workers
                .SingleOrDefaultAsync(x => x.Id == worker.WorkerId);

            if (workerToUpdate is null)
            {
                throw new NotFoundException("Worker not found");
            }

            if (!string.IsNullOrWhiteSpace(worker.FirstName))
            {
                workerToUpdate!.FirstName = worker.FirstName; 
            }

            if (!string.IsNullOrWhiteSpace(worker.LastName))
            {
                workerToUpdate!.LastName = worker.LastName;
            }

            if (!string.IsNullOrWhiteSpace(worker.Patronymic))
            {
                workerToUpdate!.Patronymic = worker.Patronymic;
            }

            if (!string.IsNullOrWhiteSpace(worker.Mail))
            {
                workerToUpdate!.Mail = worker.Mail;
            }

            await _context.SaveChangesAsync();
        }
    }
}
