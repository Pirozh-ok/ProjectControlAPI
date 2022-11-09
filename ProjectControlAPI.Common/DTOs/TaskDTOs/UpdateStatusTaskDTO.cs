using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.Common.DTOs.TaskDTOs
{
    public class UpdateStatusTaskDTO
    {
        public int Id { get; set; }
        public Status Status { get; set; } 
    }
}
