namespace ProjectControlAPI.DataAccess.Entities
{
    public class WorkerProject
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}
