using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.BusinessLogic.Services.Interfaces;
using ProjectControlAPI.Common.DTOs.WorkerDTOs;
using ProjectControlAPI.Common.Exceptions;
using ProjectControlAPI.Common.Mapping;
using ProjectControlAPI.DataAccess;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.TestBusinessLogic
{
    public class WorkerServiceTest
    {
        private static DbContextOptions<DataContext> _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "TestDB")
                    .Options;

        private DataContext _context;
        private IMapper _mapper;
        private IWorkerService _workerService; 

        public WorkerServiceTest()
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

            _workerService = new WorkerService(_context, _mapper);
        }

        [Fact]
        public async void CreateWorker_CorrectData_Success()
        {
            //Avarage

            var worker = new CreateWorkerDTO()
            {
                FirstName = "Алексей",
                LastName = "Алексеев",
                Patronymic = "Алексеевич",
                Mail = "alex@mail.ru"
            };

            // Act

            await _workerService.CreateAsync(worker);

            // Assert

            Assert.True(await _context.Workers.AnyAsync(x => x.Id == 4));
        }

        [Fact]
        public async void CreateWorker_FirstNameNull_ThrowBadRequestException()
        {
            //Avarage

            var worker = new CreateWorkerDTO()
            {
                FirstName = null,
                LastName = "Алексеев",
                Patronymic = "Алексеевич",
                Mail = "alex@mail.ru"
            };

            // Act

            Func<Task> act = () => _workerService.CreateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateWorker_FirstNameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var worker = new CreateWorkerDTO()
            {
                FirstName = "".PadLeft(150, 'a'),
                LastName = "Алексеев",
                Patronymic = "Алексеевич",
                Mail = "alex@mail.ru"
            };

            // Act

            Func<Task> act = () => _workerService.CreateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateWorker_LastNameNull_ThrowBadRequestException()
        {
            //Avarage

            var worker = new CreateWorkerDTO()
            {
                FirstName = "Алексей",
                LastName = null,
                Patronymic = "Алексеевич",
                Mail = "alex@mail.ru"
            };

            // Act

            Func<Task> act = () => _workerService.CreateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateWorker_LastNameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var worker = new CreateWorkerDTO()
            {
                FirstName = "Алексей",
                LastName = "".PadLeft(150, 'a'),
                Patronymic = "Алексеевич",
                Mail = "alex@mail.ru"
            };

            // Act

            Func<Task> act = () => _workerService.CreateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateWorker_PatronymicNull_ThrowBadRequestException()
        {
            //Avarage

            var worker = new CreateWorkerDTO()
            {
                FirstName = "Алексей",
                LastName = "Алексеев",
                Patronymic = null,
                Mail = "alex@mail.ru"
            };

            // Act

            Func<Task> act = () => _workerService.CreateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateWorker_PatronymicMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var worker = new CreateWorkerDTO()
            {
                FirstName = "Алексей",
                LastName = "Алексеев",
                Patronymic = "".PadLeft(150, 'a'),
                Mail = "alex@mail.ru"
            };

            // Act

            Func<Task> act = () => _workerService.CreateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateWorker_MailNull_ThrowBadRequestException()
        {
            //Avarage

            var worker = new CreateWorkerDTO()
            {
                FirstName = "Алексей",
                LastName = "Алексеев",
                Patronymic = "Алексеевич",
                Mail = null
            };

            // Act

            Func<Task> act = () => _workerService.CreateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateWorker_MailcMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var worker = new CreateWorkerDTO()
            {
                FirstName = "Алексей",
                LastName = "Алексеев",
                Patronymic = "Алексеевич",
                Mail = "".PadLeft(150, 'a')
            };

            // Act

            Func<Task> act = () => _workerService.CreateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateWorker_MailAlreadyExist_ThrowBadRequestException()
        {
            //Avarage

            var worker = new CreateWorkerDTO()
            {
                FirstName = "Алексей",
                LastName = "Алексеев",
                Patronymic = "Алексеевич",
                Mail = "ivanov@gmail.com"
            };

            // Act

            Func<Task> act = () => _workerService.CreateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void CreateWorker_NullArgument_ThrowBadRequestException()
        {
            //Avarage

            CreateWorkerDTO worker = null; 

            // Act

            Func<Task> act = () => _workerService.CreateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void DeleteWorker_CorrectId_Success()
        {
            //Avarage

            var workerId = 3; 

            // Act

            await _workerService.DeleteAsync(workerId);

            // Assert

            Assert.False(await _context.Workers.AnyAsync(x => x.Id == 3));
        }

        [Fact]
        public async void DeleteWorker_IncorrectId_ThrowNotFoundException()
        {
            //Avarage

            var workerId = 10;

            // Act

            Func<Task> act = () => _workerService.DeleteAsync(workerId);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void GetWorkerById_IncorrectId_ThrowNotFoundException()
        {
            //Avarage

            var workerId = 10;

            // Act

            Func<Task> act = () => _workerService.GetByIdAsync(workerId);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void UpdateWorker_CorrectData_Success()
        {
            //Avarage

            var worker = new UpdateWorkerDTO()
            {
                Id = 1,
                LastName = "Воробьёв",
                Mail = "ivan12345@mail.ru"
            };

            var expectedWorker = await _context.Workers
                .SingleOrDefaultAsync(x => x.Id == worker.Id);

            expectedWorker.LastName = "Воробьёв";
            expectedWorker.Mail = "ivan12345@mail.ru"; 

            // Act

            await _workerService.UpdateAsync(worker);

            // Assert

            Assert.Equal(await _context.Workers.SingleOrDefaultAsync(x => x.Id == 1), expectedWorker); 
        }

        [Fact]
        public async void UpdateWorker_FirstNameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var worker = new UpdateWorkerDTO()
            {
                Id = 1,
                FirstName = "".PadLeft(150, 'a'),
            };

            // Act

            Func<Task> act = () => _workerService.UpdateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateWorker_LastNameMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var worker = new UpdateWorkerDTO()
            {
                Id = 1,
                LastName = "".PadLeft(150, 'a'),
            };

            // Act

            Func<Task> act = () => _workerService.UpdateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateWorker_PatronymicMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var worker = new UpdateWorkerDTO()
            {
                Id = 1,
                Patronymic = "".PadLeft(150, 'a'),
            };

            // Act

            Func<Task> act = () => _workerService.UpdateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateWorker_MailMoreMaxLen_ThrowBadRequestException()
        {
            //Avarage

            var worker = new UpdateWorkerDTO()
            {
                Id = 1,
                Mail = "".PadLeft(150, 'a'),
            };

            // Act

            Func<Task> act = () => _workerService.UpdateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateWorker_MailAlreadyExist_ThrowBadRequestException()
        {
            //Avarage

            var worker = new UpdateWorkerDTO()
            {
                Id = 2,
                Mail = "ivanov@gmail.com"
            };

            // Act

            Func<Task> act = () => _workerService.UpdateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdateWorker_NullArgument_ThrowBadRequestException()
        {
            //Avarage

            UpdateWorkerDTO worker = null; 

            // Act

            Func<Task> act = () => _workerService.UpdateAsync(worker);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        ~WorkerServiceTest()
        {
            _context.Database.EnsureDeleted();
        }
    }
}