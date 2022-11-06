namespace ProjectControlAPI.DataAccess.Entities
{
    public enum Status
    {
        ToDo,
        InProgress,
        Done
    }

    public class TaskProject
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public Status Status { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }

        // Author worker
        public int AuthorId { get; set; }
        public Worker Author { get; set; }

        // Executor worker
        public int? WorkerId { get; set; }
        public Worker Worker { get; set; }

        // Project
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
