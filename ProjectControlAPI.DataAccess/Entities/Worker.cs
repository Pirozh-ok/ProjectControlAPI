namespace ProjectControlAPI.DataAccess.Entities
{
    public class Worker
    {
        public Worker()
        {
            Projects = new HashSet<Project>(); 
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Mail { get; set; }

        // Worker projects
        public ICollection<Project> Projects { get; set; }
    }
}
