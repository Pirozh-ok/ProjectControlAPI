using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectControlAPI.DataAccess.Entities;
using ProjectControlAPI.DataAccess.EntitiesConfigurations;

namespace ProjectControlAPI.DataAccess
{
    public class DataContext : DbContext
    {
        private readonly string _connectionString = string.Empty; 
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"]; 
        }

        public DataContext()
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=ProjectControl; Persist Security Info = false; User ID='sa';Password='sa'; MultipleActiveResultSets = True; Trusted_Connection=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new WorkerConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());
        }
    }
}
