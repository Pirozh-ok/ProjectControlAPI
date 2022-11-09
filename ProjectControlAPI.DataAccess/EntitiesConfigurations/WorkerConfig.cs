using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectControlAPI.DataAccess.Entities;
using static ProjectControlAPI.DataAccess.Entities.Worker;

namespace ProjectControlAPI.DataAccess.EntitiesConfigurations
{
    internal class WorkerConfig : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .HasIndex(x => x.Mail)
                .IsUnique();

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Patronymic)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Mail)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasDefaultValue(WorkerRole.Employee);

            builder
                .HasMany(w => w.WorkerProject)
                .WithOne(wp => wp.Worker)
                .HasForeignKey(wp => wp.WorkerId);

            builder
                .HasMany(w => w.CreatedTasks)
                .WithOne(t => t.Author)
                .HasForeignKey(t => t.AuthorId);

            builder
                .HasMany(w => w.ExecutedTasks)
                .WithOne(t => t.Worker)
                .HasForeignKey(t => t.WorkerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
