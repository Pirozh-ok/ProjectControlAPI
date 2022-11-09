using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs.ProjectDTOs;
using ProjectControlAPI.Common.DTOs.TaskDTOs;
using ProjectControlAPI.Common.DTOs.WorkerDTOs;
using ProjectControlAPI.Common.Exceptions;
using ProjectControlAPI.Common.Resource;
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
            var workerToAdd = _mapper.Map<Worker>(worker);

            GuardIncorrectData(workerToAdd);
            await GuardMailAlreadyExistAsync(worker.Mail);
            
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
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GetWorkerDTO> GetByIdAsync(int workerId)
        {
            var worker = await _context.Workers
                .AsNoTracking()
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
                await GuardMailAlreadyExistAsync(worker.Mail);
                workerToUpdate.Mail = worker.Mail;
            }

            GuardIncorrectData(workerToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetProjectDTO>> GetProjectsByWorkerAsync(int workerId)
        {
            var worker = await _context.Workers
                .SingleOrDefaultAsync(x => x.Id == workerId);

            GuardNotFound(worker);

            var projectsId = await _context.WorkerProject
                .Where(x => x.WorkerId == workerId)
                .AsNoTracking()
                .Select(x => x.ProjectId)
                .ToListAsync();

            return await _context.Projects
                .Where(x => projectsId.Contains(x.Id))
                .AsNoTracking()
                .ProjectTo<GetProjectDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(); 
        }

        public async Task<IEnumerable<GetTaskDTO>> GetCreatedTasksByWorkerAsync(int workerId)
        {
            if (await _context.Workers.AnyAsync(x => x.Id == workerId))
            {
                return await _context.TaskProject
                    .Where(x => x.AuthorId == workerId)
                    .ProjectTo<GetTaskDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            else
            {
                throw new BadRequestException(WorkerMessageResource.NotFound);
            }
        }

        public async Task<IEnumerable<GetTaskDTO>> GetPerfomeTasksByWorkerAsync(int workerId)
        {
            if (await _context.Workers.AnyAsync(x => x.Id == workerId))
            {
                return await _context.TaskProject
                    .Where(x => x.WorkerId == workerId)
                    .ProjectTo<GetTaskDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            else
            {
                throw new BadRequestException(WorkerMessageResource.NotFound);
            }
        }

        private void GuardNullArgument<T>(T arg)
        {
            if (arg is null)
            {
                throw new BadRequestException(WorkerMessageResource.NullArgument);
            }
        }

        private void GuardNotFound(Worker? worker)
        {
            if (worker is null)
            {
                throw new NotFoundException(WorkerMessageResource.NotFound);
            }
        }

        private void GuardNullOrEmptyString(string str, string message)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new BadRequestException(message);
            }
        }

        private void GuardExceedMaxLen(string str, string message, int maxLen)
        {
            if (str.Length > maxLen)
            {
                throw new BadRequestException(message);
            }
        }

        private async Task GuardMailAlreadyExistAsync(string mail)
        {
            if (!await _context.Workers.AnyAsync(x => x.Mail == mail))
            {
                throw new BadRequestException(WorkerMessageResource.MailAlreadyExists);
            }
        }

        private void GuardIncorrectData(Worker worker)
        {
            GuardNullArgument(worker);

            GuardNullOrEmptyString(worker.FirstName, WorkerMessageResource.IncorrectFirstName);
            GuardExceedMaxLen(worker.FirstName, WorkerMessageResource.FirstNameExceedsMaxLen, 50);

            GuardNullOrEmptyString(worker.LastName, WorkerMessageResource.IncorrectLastName);
            GuardExceedMaxLen(worker.LastName, WorkerMessageResource.LastNameExceedsMaxLen, 50);

            GuardNullOrEmptyString(worker.Patronymic, WorkerMessageResource.IncorrectPatronymic);
            GuardExceedMaxLen(worker.Patronymic, WorkerMessageResource.PatronymicExceedsMaxLen, 50);

            GuardNullOrEmptyString(worker.Mail, WorkerMessageResource.IncorrectMail);
            GuardExceedMaxLen(worker.Mail, WorkerMessageResource.MailExceedsMaxLen, 100);
        }
    }
}
