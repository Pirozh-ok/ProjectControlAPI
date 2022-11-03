namespace ProjectControlAPI.DataAccess.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Workers of company
        public ICollection<Worker> Workers { get; set; }
    }
}
