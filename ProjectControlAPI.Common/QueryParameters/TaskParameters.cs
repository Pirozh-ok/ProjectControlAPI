using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.Common.QueryParameters
{
    public class TaskParameters
    {
        public Status? Status { get; set; } 
        public uint MaxPriority { get; set; } = uint.MaxValue;
        public uint MinPriority { get; set; } = uint.MinValue;
        public string OrderBy { get; set; } = "name asc";
    }
}
