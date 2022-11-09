using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.BusinessLogic.Services.Interfaces;
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

        ~ProjectServiceTest()
        {
            _context.Database.EnsureDeleted(); 
        }
    }
}
