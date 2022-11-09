using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectControlAPI.BusinessLogic.Services.Extensions;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs.TaskDTOs;
using ProjectControlAPI.Common.Exceptions;
using ProjectControlAPI.Common.QueryParameters;
using ProjectControlAPI.Common.Resource;
using ProjectControlAPI.DataAccess;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.BusinessLogic.Services.Interfaces
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper; 

        public TaskService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateTaskDTO task)
        {
            var taskToAdd = _mapper.Map<TaskProject>(task);

            GuardIncorrectData(taskToAdd);

            await _context.TaskProject.AddAsync(taskToAdd);
            await _context.SaveChangesAsync(); 
        }

        public async Task<GetTaskDTO> GetByIdAsync(int taskId)
        {
            var task = await _context.TaskProject
                .SingleOrDefaultAsync(x => x.Id == taskId);

            GuardNotFound(task);

            return _mapper.Map<GetTaskDTO>(task);
        }

        public async Task DeleteAsync(int taskId)
        {
            var task = await _context.TaskProject
                .SingleOrDefaultAsync(x => x.Id == taskId);

            GuardNotFound(task);

            _context.TaskProject.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetTaskDTO>> GetAllAsync(TaskParameters parameters)
        {
            var tasks =  await _context.TaskProject              
                .ProjectTo<GetTaskDTO>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .Where(x => x.Priority <= parameters.MaxPriority
                && x.Priority >= parameters.MinPriority)
                .ToListAsync();
            
            if(parameters.Status is not null 
                && parameters.Status >= Status.ToDo
                && parameters.Status <= Status.Done)
            {
                tasks = tasks.Where(x => x.Status == parameters.Status).ToList();
            }

            return tasks.OrderByField(parameters.OrderBy);
        }

        public async Task UpdateAsync(UpdateTaskDTO task)
        {
            GuardNullArgument(task);

            var taskToUpdate = await _context.TaskProject
                .SingleOrDefaultAsync(x => x.Id == task.Id);
            
            GuardNotFound(taskToUpdate);

            if(!string.IsNullOrEmpty(task.Name))
            {
                taskToUpdate!.Name = task.Name;
            }

            if (!string.IsNullOrEmpty(task.Comment))
            {
                taskToUpdate!.Comment = task.Comment;
            }

            if (task.Status is not null)
            {
                taskToUpdate!.Status = (Status)task.Status;
            }

            if (task.WorkerId is not null)
            {
                taskToUpdate!.WorkerId = (int)task.WorkerId;
            }

            if (task.Priority is not null)
            {
                taskToUpdate!.Priority = (int)task.Priority;
            }

            GuardIncorrectData(taskToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(UpdateStatusTaskDTO task)
        {
            GuardNullArgument(task);

            var taskToUpdate = await _context.TaskProject
                .SingleOrDefaultAsync(x => x.Id == task.Id);

            GuardNotFound(taskToUpdate);
            GuardIncorrectStatus(task.Status);

            taskToUpdate.Status = task.Status;
            await _context.SaveChangesAsync(); 
        }

        private void GuardIncorrectData(TaskProject task)
        {
            GuardNullArgument(task);

            GuardNullOrEmptyString(task.Name, TaskMessageResource.IncorrectName);
            GuardExceedMaxLen(task.Name, TaskMessageResource.NameTaskExceedsMaxLen, 50);

            GuardIncorrectComment(task.Comment);
            GuardIncorrectStatus(task.Status);
            GuardIncorrectPriority(task.Priority);
            GuardNotFoundAuthor(task.AuthorId);
            GuardNotFoundWorker(task.WorkerId);
            GuardNotFoundProject(task.ProjectId);
        }

        private void GuardIncorrectComment(string comment)
        {
            if(comment is not null)
            {
                if(comment.Length > 100)
                {
                    throw new BadRequestException(TaskMessageResource.CommentExceedsMaxLen);
                }
            }
        }

        private void GuardIncorrectStatus(Status status)
        {
            if(status > Status.Done || status < Status.ToDo)
            {
                throw new BadRequestException(TaskMessageResource.IncorrectStatus);
            }
        }

        private void GuardNotFoundAuthor(int id)
        {
            if (!_context.Workers.Any(x => x.Id == id))
            {
                throw new NotFoundException(TaskMessageResource.NotFoundAuthor);
            }
        }

        private void GuardNotFoundProject(int id)
        {
            if (!_context.Projects.Any(x => x.Id == id))
            {
                throw new NotFoundException(ProjectMessageResource.NotFound);
            }
        }

        private void GuardNotFoundWorker(int? id)
        {
            if (id is not null && !_context.Workers.Any(x => x.Id == id))
            {
                throw new NotFoundException(TaskMessageResource.NotFoundWorker);
            }
        }

        private void GuardNullArgument<T>(T arg)
        {
            if (arg is null)
            {
                throw new BadRequestException(TaskMessageResource.NullArgument);
            }
        }

        private void GuardNotFound(TaskProject? task)
        {
            if (task is null)
            {
                throw new NotFoundException(TaskMessageResource.NotFound);
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

        private void GuardIncorrectPriority(int priority)
        {
            if (priority < 0)
            {
                throw new BadRequestException(TaskMessageResource.IncorrectPriority);
            }
        }
    }
}
