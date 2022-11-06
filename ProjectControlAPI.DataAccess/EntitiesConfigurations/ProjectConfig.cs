using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.DataAccess.EntitiesConfigurations
{
    internal class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired(); 

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.CustomerCompanyName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ExecutorCompanyName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.StartDate)
                .IsRequired(); 

            builder.Property(x => x.EndDate)
                .IsRequired();

            builder
               .HasMany(p => p.WorkerProject)
               .WithOne(wp => wp.Project)
               .HasForeignKey(wp => wp.ProjectId);

            builder
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
