using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.BusinessLogic.Services.Interfaces;
using ProjectControlAPI.Common.DTOs.TaskDTOs;
using ProjectControlAPI.Common.Exceptions;
using ProjectControlAPI.Common.Mapping;
using ProjectControlAPI.DataAccess;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.TestBusinessLogic
{
    public class TaskServiceTest
    {
        private static DbContextOptions<DataContext> _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "TestDB")
                    .Options;

        private DataContext _context;
        private IMapper _mapper;
        private ITaskService _taskService;

        public TaskServiceTest()
        {
            _context = new DataContext(_dbContextOptions);
            _context.Database.EnsureCreated();
            _context.AddTestData();

            _mapper = new Mapper(new MapperConfiguration(
                options =>
                {
                    options.AddProfile(typeof(WorkerProfile));
                    options.AddProfile(typeof(ProjectProfile));
                    options.AddProfile(typeof(TaskProfile));
                }));

            _taskService = new TaskService(_context, _mapper);
        }

        [Fact]
        public async void CreateTask_CorrectData_Success()
        {
            //Avarage

            var task = new CreateTaskDTO()
            {
                Name = "Добавить ...",
                Status = Status.ToDo,
                Comment = "Комментарий",
                Priority = 2,
                ProjectId = 2,
                AuthorId = 2,
                WorkerId = 1
            };

            // Act

            await _taskService.CreateAsync(task);

            // Assert

            Assert.True(await _context.TaskProject.AnyAsync(x => x.Id == 3));
        }

        [Fact]
        public async void CreateTask_NullArgument_ThrowBadRequestException()
        {
            //Avarage

            CreateTaskDTO task = null;

            // Act

            Func<Task> act = () => _taskService.CreateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateTask_NullName_ThrowBadRequestException()
        {
            //Avarage

            var task = new CreateTaskDTO()
            {
                Name = null,
                Status = Status.ToDo,
                Comment = "Комментарий",
                Priority = 2,
                ProjectId = 2,
                AuthorId = 2,
                WorkerId = 1
            };

            // Act

            Func<Task> act = () => _taskService.CreateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateTask_NameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var task = new CreateTaskDTO()
            {
                Name = "".PadLeft(150, 'a'),
                Status = Status.ToDo,
                Comment = "Комментарий",
                Priority = 2,
                ProjectId = 2,
                AuthorId = 2,
                WorkerId = 1
            };

            // Act

            Func<Task> act = () => _taskService.CreateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateTask_CommentMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var task = new CreateTaskDTO()
            {
                Name = "Добавить ...",
                Status = Status.ToDo,
                Comment = "".PadLeft(150, 'a'),
                Priority = 2,
                ProjectId = 2,
                AuthorId = 2,
                WorkerId = 1
            };

            // Act

            Func<Task> act = () => _taskService.CreateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateTask_IncorrectStatus_ThrowBadRequestException()
        {
            //Avarage

            var task = new CreateTaskDTO()
            {
                Name = "Добавить ...",
                Status = (Status)10,
                Comment = "Комментарий",
                Priority = 2,
                ProjectId = 2,
                AuthorId = 2,
                WorkerId = 1
            };

            // Act

            Func<Task> act = () => _taskService.CreateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateTask_IncorrectPriority_ThrowBadRequestException()
        {
            //Avarage

            var task = new CreateTaskDTO()
            {
                Name = "Добавить ...",
                Status = Status.ToDo,
                Comment = "Комментарий",
                Priority = -1,
                ProjectId = 2,
                AuthorId = 2,
                WorkerId = 1
            };

            // Act

            Func<Task> act = () => _taskService.CreateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateTask_IncorrectProjectId_ThrowNotFoundException()
        {
            //Avarage

            var task = new CreateTaskDTO()
            {
                Name = "Добавить ...",
                Status = Status.ToDo,
                Comment = "Комментарий",
                Priority = 2,
                ProjectId = 10,
                AuthorId = 2,
                WorkerId = 1
            };

            // Act

            Func<Task> act = () => _taskService.CreateAsync(task);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void CreateTask_IncorrectAuthorId_ThrowNotFoundException()
        {
            //Avarage

            var task = new CreateTaskDTO()
            {
                Name = "Добавить ...",
                Status = Status.ToDo,
                Comment = "Комментарий",
                Priority = 2,
                ProjectId = 2,
                AuthorId = 100,
                WorkerId = 1
            };

            // Act

            Func<Task> act = () => _taskService.CreateAsync(task);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void CreateTask_IncorrectWorkerId_ThrowNotFoundException()
        {
            //Avarage

            var task = new CreateTaskDTO()
            {
                Name = "Добавить ...",
                Status = Status.ToDo,
                Comment = "Комментарий",
                Priority = 2,
                ProjectId = 2,
                AuthorId = 2,
                WorkerId = 100
            };

            // Act

            Func<Task> act = () => _taskService.CreateAsync(task);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void DeleteTask_CorrectId_Success()
        {
            //Avarage

            var taskId = 1;

            // Act

            await _taskService.DeleteAsync(taskId);

            // Assert

            Assert.False(await _context.TaskProject.AnyAsync(x => x.Id == taskId));
        }

        [Fact]
        public async void DeleteTask_IncorrectId_ThrowNotFoundException()
        {
            //Avarage

            var taskId = 100;

            // Act

            Func<Task> act = () => _taskService.DeleteAsync(taskId);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void GetTaskById_IncorrectId_ThrowNotFoundException()
        {
            //Avarage

            var taskId = 100;

            // Act

            Func<Task> act = () => _taskService.GetByIdAsync(taskId);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void UpdateTask_CorrectData_Success()
        {
            //Avarage

            var task = new UpdateTaskDTO()
            {
                Id = 1,
                Status = Status.ToDo
            };

            var expected = await _context.TaskProject
                .SingleOrDefaultAsync(x => x.Id == task.Id);

            expected.Status = Status.ToDo; 

            // Act

            await _taskService.UpdateAsync(task);

            // Assert

            Assert.Equal(await _context.TaskProject.SingleOrDefaultAsync(x => x.Id == task.Id), expected);
        }

        [Fact]
        public async void UpdateTask_NameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var task = new UpdateTaskDTO()
            {
                Id = 1,
                Name = "".PadLeft(150, 'a')
            };

            // Act

            Func<Task> act = () => _taskService.UpdateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateTask_CommentMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var task = new UpdateTaskDTO()
            {
                Id = 1,
                Comment = "".PadLeft(200, 'a')
            };

            // Act

            Func<Task> act = () => _taskService.UpdateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateTask_IncorrectStatus_ThrowBadRequestException()
        {
            //Avarage

            var task = new UpdateTaskDTO()
            {
                Id = 1,
                Status = (Status)10
            };

            // Act

            Func<Task> act = () => _taskService.UpdateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateTask_IncorrectPriority_ThrowBadRequestException()
        {
            //Avarage

            var task = new UpdateTaskDTO()
            {
                Id = 1,
                Priority = -1
            };

            // Act

            Func<Task> act = () => _taskService.UpdateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateTask_IncorrectWorkerId_ThrowNotFoundException()
        {
            //Avarage

            var task = new UpdateTaskDTO()
            {
                Id = 1,
                WorkerId = 100
            };

            // Act

            Func<Task> act = () => _taskService.UpdateAsync(task);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void UpdateTask_IncorrectId_ThrowNotFoundException()
        {
            //Avarage

            var task = new UpdateTaskDTO()
            {
                Id = 100,
                Name = "Задача"
            };

            // Act

            Func<Task> act = () => _taskService.UpdateAsync(task);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void UpdateTask_NullArgument_ThrowBadRequestException()
        {
            //Avarage

            UpdateTaskDTO task = null; 

            // Act

            Func<Task> act = () => _taskService.UpdateAsync(task);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }
    }
}
