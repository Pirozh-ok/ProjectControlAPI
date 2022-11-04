using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.Common.DTOs
{
    public class GetWorkerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Mail { get; set; }
        public List<Project> Projects { get; set; }
    }
}
