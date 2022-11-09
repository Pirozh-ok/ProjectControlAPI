namespace ProjectControlAPI.DataAccess.Entities
{
    public class Worker
    {
        public enum WorkerRole
        {
            Director,
            ProjectManager,
            Employee
        }

        public Worker()
        {
            WorkerProject = new HashSet<WorkerProject>();
            CreatedTasks = new HashSet<TaskProject>();
            ExecutedTasks = new HashSet<TaskProject>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Mail { get; set; }
        public WorkerRole Role { get; set; }

        // Worker projects
        public ICollection<WorkerProject> WorkerProject { get; set; }
        
        // Author tasks
        public ICollection<TaskProject> CreatedTasks { get; set; }

        // Worker tasks
        public ICollection<TaskProject> ExecutedTasks { get; set; }
    }
}
