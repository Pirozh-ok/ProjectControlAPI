using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.DataAccess.EntitiesConfigurations
{
    public class WorkerProjectConfig : IEntityTypeConfiguration<WorkerProject>
    {
        public void Configure(EntityTypeBuilder<WorkerProject> builder)
        {
            builder.HasKey(x => new { x.WorkerId, x.ProjectId });

            builder.Property(x => x.Position)
                .HasDefaultValue(Position.Employee);
        }
    }
}
