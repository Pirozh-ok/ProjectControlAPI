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
                .HasMany(x => x.Workers)
                .WithMany(x => x.Projects);
        }
    }
}
