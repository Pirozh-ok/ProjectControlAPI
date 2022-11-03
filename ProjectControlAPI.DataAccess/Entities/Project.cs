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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }

        // Company of customer
        public int CustomerId { get; set; }
        public Company Customer { get; set; }

        // Company of executors
        public int ExecutorId { get; set; }
        public Company Executor { get; set; }

        // Project staff
        public ICollection<Worker> Workers { get; set; }

        // Project manager
        public Worker ProjectManagerId { get; set; }
        public Worker ProjectManager { get; set; }
    }
}
