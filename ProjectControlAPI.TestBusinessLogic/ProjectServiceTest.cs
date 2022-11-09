using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.BusinessLogic.Services.Interfaces;
using ProjectControlAPI.Common.DTOs.ProjectDTOs;
using ProjectControlAPI.Common.Exceptions;
using ProjectControlAPI.Common.Mapping;
using ProjectControlAPI.DataAccess;

namespace ProjectControlAPI.TestBusinessLogic
{
    public class ProjectServiceTest
    {
        private static DbContextOptions<DataContext> _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "TestDB")
                    .Options;

        private DataContext _context;
        private IMapper _mapper;
        private IProjectService _projectService;

        public ProjectServiceTest()
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

            _projectService = new ProjectService(_context, _mapper);
        }

        [Fact]
        public async void CreateProject_CorrectData_Success()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = "Проект 3",
                CustomerCompanyName = "Компания 4",
                ExecutorCompanyName = "Компания 2",
                Priority = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(19)
            };

            // Act

            await _projectService.CreateAsync(project);

            // Assert

            Assert.True(await _context.Projects.AnyAsync(x => x.Id == 3));
        }

        [Fact]
        public async void CreateProject_NullArgument_ThrowBadRequestException()
        {
            //Avarage

            CreateProjectDTO project = null;

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateProject_NullName_ThrowBadRequestException()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = null,
                CustomerCompanyName = "Компания 4",
                ExecutorCompanyName = "Компания 2",
                Priority = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(19)
            };

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateProject_NullCustomerCompanyName_ThrowBadRequestException()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = "Проект 3",
                CustomerCompanyName = null,
                ExecutorCompanyName = "Компания 2",
                Priority = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(19)
            };

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateProject_NullExecutorCompanyName_ThrowBadRequestException()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = "Проект 3",
                CustomerCompanyName = "Компания 4",
                ExecutorCompanyName = null,
                Priority = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(19)
            };

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateProject_NameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = "".PadLeft(150, 'a'),
                CustomerCompanyName = "Компания 4",
                ExecutorCompanyName = "Компания 2",
                Priority = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(19)
            };

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateProject_ExecutorCompanyNameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = "Проект 3",
                CustomerCompanyName = "Компания 4",
                ExecutorCompanyName = "".PadLeft(150, 'a'),
                Priority = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(19)
            };

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateProject_CustomerCompanyNameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = "Проект 3",
                CustomerCompanyName = "".PadLeft(150, 'a'),
                ExecutorCompanyName = "Компания 4",
                Priority = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(19)
            };

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateProject_IncorrectDataStart_ThrowBadRequestException()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = "Проект 3",
                CustomerCompanyName = "Компания 3",
                ExecutorCompanyName = "Компания 4",
                Priority = 1,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.Now.AddMonths(19)
            };

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateProject_IncorrectDataEnd_ThrowBadRequestException()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = "Проект 3",
                CustomerCompanyName = "Компания 3",
                ExecutorCompanyName = "Компания 4",
                Priority = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.MinValue
            };

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateProject_DataEndLessDataStart_ThrowBadRequestException()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = "Проект 3",
                CustomerCompanyName = "Компания 3",
                ExecutorCompanyName = "Компания 4",
                Priority = 1,
                StartDate = DateTime.Now.AddMonths(10),
                EndDate = DateTime.Now
            };

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateProject_IncorrectPriority_ThrowBadRequestException()
        {
            //Avarage

            var project = new CreateProjectDTO()
            {
                Name = "Проект 3",
                CustomerCompanyName = "Компания 3",
                ExecutorCompanyName = "Компания 4",
                Priority = -1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(10)
            };

            // Act

            Func<Task> act = () => _projectService.CreateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void DeleteProject_CorrectId_Success()
        {
            //Avarage

            var projectId = 2;

            // Act

            await _projectService.DeleteAsync(projectId);

            // Assert

            Assert.False(await _context.Projects.AnyAsync(x => x.Id == 2));
        }

        [Fact]
        public async void DeleteProject_IncorrectId_ThrowNotFoundException()
        {
            //Avarage

            var projectId = 10;

            // Act

            Func<Task> act = () => _projectService.DeleteAsync(projectId);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void GetProjectById_IncorrectId_ThrowNotFoundException()
        {
            //Avarage

            var projectId = 10;

            // Act

            Func<Task> act = () => _projectService.GetByIdAsync(projectId);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void GetWorkersByProjectId_IncorrectId_ThrowNotFoundException()
        {
            //Avarage

            var projectId = 10;

            // Act

            Func<Task> act = () => _projectService.GetWorkersByProjectAsync(projectId);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void GetTasksByProjectId_IncorrectId_ThrowNotFoundException()
        {
            //Avarage

            var projectId = 10;

            // Act

            Func<Task> act = () => _projectService.GetByIdAsync(projectId);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void AddWorkerOnProject_CorrectData_Success()
        {
            //Avarage

            var projectId = 2;
            var workerId = 1;

            // Act

            await _projectService.AddWorkerOnProject(projectId, workerId);

            // Assert

            Assert.True(await _context.WorkerProject.AnyAsync(x => x.ProjectId == projectId && x.WorkerId == workerId));
        }

        [Fact]
        public async void AddWorkerOnProject_IncorrectProjectId_ThrowNotFoundException()
        {
            //Avarage

            var projectId = 10;
            var workerId = 1;

            // Act

            Func<Task> act = () => _projectService.AddWorkerOnProject(projectId, workerId);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void AddWorkerOnProject_InCorrectWorkerId_ThrowNotFoundException()
        {
            //Avarage

            var projectId = 2;
            var workerId = 10;

            // Act

            Func<Task> act = () => _projectService.AddWorkerOnProject(projectId, workerId);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void AddWorkerOnProject_WorkerAlreadyAddedToProject_ThrowBadRequestException()
        {
            //Avarage

            var projectId = 1;
            var workerId = 3;

            // Act

            Func<Task> act = () => _projectService.AddWorkerOnProject(projectId, workerId);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateProject_CorrectData_Success()
        {
            //Avarage

            var project = new UpdateProjectDTO()
            {
                Id = 1,
                Name = "Проект 6",
                CustomerCompanyName = "Компания 1",
                ExecutorCompanyName = "Компания 2",
                Priority = 5,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(32)
            };

            var expected = await _context.Projects
            .SingleOrDefaultAsync(x => x.Id == project.Id);

            expected.Name = "Проект 6";
            expected.CustomerCompanyName = "Компания 1";
            expected.ExecutorCompanyName = "Компания 2";
            expected.Priority = 5;
            expected.StartDate = DateTime.Now;
            expected.EndDate = DateTime.Now.AddMonths(32);

            // Act

            await _projectService.UpdateAsync(project);

            // Assert

            Assert.Equal(await _context.Projects.SingleOrDefaultAsync(x => x.Id == project.Id), expected);
        }

        [Fact]
        public async void UpdateProject_NameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var project = new UpdateProjectDTO()
            {
                Id = 1,
                Name = "".PadLeft(150, 'a')
            };

            // Act

            Func<Task> act = () => _projectService.UpdateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateProject_ExecutorCompanyNameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var project = new UpdateProjectDTO()
            {
                Id = 1,
                ExecutorCompanyName = "".PadLeft(150, 'a')
            };

            // Act

            Func<Task> act = () => _projectService.UpdateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateProject_CustomerCompanyNameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var project = new UpdateProjectDTO()
            {
                Id = 1,
                CustomerCompanyName = "".PadLeft(150, 'a')
            };

            // Act

            Func<Task> act = () => _projectService.UpdateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateProject_IncorrectDataStart_ThrowBadRequestException()
        {
            //Avarage

            var project = new UpdateProjectDTO()
            {
                Id = 1,
                StartDate = DateTime.MinValue
            };

            // Act

            Func<Task> act = () => _projectService.UpdateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateProject_IncorrectDataEnd_ThrowBadRequestException()
        {
            //Avarage

            var project = new UpdateProjectDTO()
            {
                Id = 1,
                EndDate = DateTime.MinValue
            };

            // Act

            Func<Task> act = () => _projectService.UpdateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateProject_DataEndLessDataStart_ThrowBadRequestException()
        {
            //Avarage

            var project = new UpdateProjectDTO()
            {
                Id = 1,
                StartDate = DateTime.Now.AddDays(10),
                EndDate = DateTime.Now
            };

            // Act

            Func<Task> act = () => _projectService.UpdateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateProject_IncorrectPriority_ThrowBadRequestException()
        {
            //Avarage

            var project = new UpdateProjectDTO()
            {
                Id = 1,
                Priority = -100,
            };

            // Act

            Func<Task> act = () => _projectService.UpdateAsync(project);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateProject_IncorrectId_ThrowNotFound()
        {
            //Avarage

            var project = new UpdateProjectDTO()
            {
                Id = 100,
            };

            // Act

            Func<Task> act = () => _projectService.UpdateAsync(project);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        ~ProjectServiceTest()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
