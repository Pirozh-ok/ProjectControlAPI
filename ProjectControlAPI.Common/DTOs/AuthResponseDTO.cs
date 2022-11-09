using static ProjectControlAPI.DataAccess.Entities.Worker;

namespace ProjectControlAPI.Common.DTOs
{
    public class AuthResponseDTO
    {
        public int Id { get; set; }
        public WorkerRole Role { get; set; }
        public string Mail { get; set; }
    }
}
