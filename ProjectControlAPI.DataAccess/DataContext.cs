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
        public DbSet<WorkerProject> WorkerProject { get; set; }
        public DbSet<TaskProject> TaskProject { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new WorkerConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());
            modelBuilder.ApplyConfiguration(new WorkerProjectConfig());
        }
    }
}
