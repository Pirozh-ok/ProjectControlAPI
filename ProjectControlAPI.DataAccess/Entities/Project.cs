namespace ProjectControlAPI.DataAccess.Entities
{
    public class Project
    {
        public Project()
        {
            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerCompanyName { get; set; }
        public string ExecutorCompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }

        // Project staff
        public ICollection<Worker> Workers { get; set; }

        // Project manager
        public int ProjectManagerId { get; set; }
        public Worker ProjectManager { get; set; }
    }
}
