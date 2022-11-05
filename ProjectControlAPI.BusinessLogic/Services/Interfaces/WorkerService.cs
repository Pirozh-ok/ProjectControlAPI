using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs.WorkerDTOs;
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
            await GuardIncorrectData(worker);

            var workerToAdd = _mapper.Map<Worker>(worker);
            await _context.Workers.AddAsync(workerToAdd);
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(int workerId)
        {
            var worker = await _context.Workers
                .SingleOrDefaultAsync(x => x.Id == workerId);

            GuardNotFound(worker);

            _context.Workers.Remove(worker!);
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

            GuardNotFound(worker);

            return _mapper.Map<GetWorkerDTO>(worker); 
        }

        public async Task UpdateAsync(UpdateWorkerDTO worker)
        {
            GuardNullArgument(worker);

            var workerToUpdate = await _context.Workers
                .SingleOrDefaultAsync(x => x.Id == worker.Id);

            GuardNotFound(workerToUpdate); 

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

        private void GuardNullArgument<T>(T arg)
        {
            if (arg is null)
            {
                throw new BadRequestException("Argument is null");
            }
        }

        private void GuardNotFound(Worker? worker)
        {
            if (worker is null)
            {
                throw new NotFoundException("Worker is not found");
            }
        }

        private async Task GuardIncorrectData(CreateWorkerDTO worker)
        {
            if (worker is null)
            {
                throw new BadRequestException("Null arguments");
            }

            if (string.IsNullOrWhiteSpace(worker.FirstName))
            {
                throw new BadRequestException("Incorrect FirstName");
            }

            if (worker.FirstName.Length > 50)
            {
                throw new BadRequestException("The first name exceeds 100 characters");
            }

            if (string.IsNullOrWhiteSpace(worker.LastName))
            {
                throw new BadRequestException("Incorrect LastName");
            }

            if (worker.LastName.Length > 50)
            {
                throw new BadRequestException("The last name exceeds 100 characters");
            }

            if (string.IsNullOrWhiteSpace(worker.Patronymic))
            {
                throw new BadRequestException("Incorrect Patronymic");
            }

            if (worker.Patronymic.Length > 50)
            {
                throw new BadRequestException("The patronymic exceeds 100 characters");
            }

            if (string.IsNullOrWhiteSpace(worker.Mail))
            {
                throw new BadRequestException("Incorrect Mail");
            }

            if (worker.Mail.Length > 50)
            {
                throw new BadRequestException("The mail exceeds 100 characters");
            }

            if(await _context.Workers.SingleOrDefaultAsync(x => x.Mail == worker.Mail) is not null)
            {
                throw new BadRequestException("A worker with this mail already exists");
            }
        }
    }
}
