using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.Common.DTOs.TaskDTOs
{
    public class GetTaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }
        public int AuthorId { get; set; }
        public int WorkerId { get; set; }
        public int ProjectId { get; set; }
    }
}
