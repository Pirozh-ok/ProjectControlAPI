namespace ProjectControlAPI.DataAccess.Entities
{
    public class Worker
    {
        public Worker()
        {
            WorkerProject = new HashSet<WorkerProject>(); 
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Mail { get; set; }

        // Worker projects
        public ICollection<WorkerProject> WorkerProject { get; set; }
    }
}
