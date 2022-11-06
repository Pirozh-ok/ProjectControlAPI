using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.DataAccess.EntitiesConfigurations
{
    public class TaskConfig : IEntityTypeConfiguration<TaskProject>
    {
        public void Configure(EntityTypeBuilder<TaskProject> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Comment)
                .HasMaxLength(100)
                .HasDefaultValue("");

            builder.Property(x => x.Priority)
                .HasDefaultValue(0);

            builder.Property(x => x.Status)
                .HasDefaultValue(Status.ToDo); 
        }
    }
}
