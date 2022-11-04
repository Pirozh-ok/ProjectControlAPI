using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs;
using ProjectControlAPI.Common.Exceptions;
using ProjectControlAPI.DataAccess;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.BusinessLogic.Services.Interfaces
{
    public class WorkerService : IWorkerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper; 

        public WorkerService(DataContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateWorkerDTO worker)
        {
            GuardIncorrectData(worker);

            var workerToAdd = _mapper.Map<Worker>(worker);
            await _context.Workers.AddAsync(workerToAdd);
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

        public async Task<IEnumerable<GetWorkerDTO>> GetAllAsync()
        {
            return await _context.Workers
                .ProjectTo<GetWorkerDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GetWorkerDTO> GetByIdAsync(int workerId)
        {
            var worker = await _context.Workers
                .SingleOrDefaultAsync(x => x.Id == workerId);

            if(worker is null)
            {
                throw new NotFoundException("Worker is not found");
            }

            return _mapper.Map<GetWorkerDTO>(worker); 
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
                workerToUpdate.FirstName = worker.FirstName; 
            }

            if (!string.IsNullOrWhiteSpace(worker.LastName))
            {
                workerToUpdate.LastName = worker.LastName;
            }

            if (!string.IsNullOrWhiteSpace(worker.Patronymic))
            {
                workerToUpdate.Patronymic = worker.Patronymic;
            }

            if (!string.IsNullOrWhiteSpace(worker.Mail))
            {
                workerToUpdate.Mail = worker.Mail;
            }

            await _context.SaveChangesAsync();
        }

        private void GuardIncorrectData(CreateWorkerDTO worker)
        {
            if (worker is null)
            {
                throw new BadRequestException("Null arguments");
            }

            if (string.IsNullOrWhiteSpace(worker.FirstName))
            {
                throw new BadRequestException("Incorrect FirstName");
            }

            if (string.IsNullOrWhiteSpace(worker.LastName))
            {
                throw new BadRequestException("Incorrect LastName");
            }

            if (string.IsNullOrWhiteSpace(worker.Patronymic))
            {
                throw new BadRequestException("Incorrect Patronymic");
            }

            if (string.IsNullOrWhiteSpace(worker.Mail))
            {
                throw new BadRequestException("Incorrect Mail");
            }
        }
    }
}
