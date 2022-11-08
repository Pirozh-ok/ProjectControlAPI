using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.Common.DTOs.TaskDTOs
{
    public class CreateTaskDTO
    {
        public string Name { get; set; }
        public Status Status { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }
        public int ProjectId { get; set; }
        public int AuthorId { get; set; }
        public int? WorkerId { get; set; }
    }
}
