namespace ProjectControlAPI.Common.DTOs.ProjectDTOs
{
    public class CreateProjectDTO
    {
        public string Name { get; set; }
        public string CustomerCompanyName { get; set; }
        public string ExecutorCompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
    }
}
