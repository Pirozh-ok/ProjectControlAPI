namespace ProjectControlAPI.Common.QueryParameters
{
    public class ProjectsParameters
    {
        public uint MinYearOfStart { get; set; } = 0;
        public uint MaxYearOfStart { get; set; } = uint.MaxValue;
        public bool ValidYearRange => MaxYearOfStart > MinYearOfStart;
        public uint MinYearOfEnd { get; set; } = 0;
        public uint MaxYearOfEnd { get; set; } = uint.MaxValue; 
        public bool ValidYearEnd => MaxYearOfEnd > MinYearOfEnd;
        public uint MaxPriority { get; set; } = uint.MaxValue;
        public uint MinPriority { get; set; } = uint.MinValue;
        public string OrderBy { get; set; } = "name asc"; 
    }
}
