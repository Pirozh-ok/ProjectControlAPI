namespace ProjectControlAPI.Common.DTOs
{
    public class UpdateWorkerDTO
    {
        public int WorkerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? Mail { get; set; }
    }
}
