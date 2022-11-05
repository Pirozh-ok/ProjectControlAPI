using ProjectControlAPI.Common.DTOs.ProjectDTOs;

namespace ProjectControlAPI.BusinessLogic.Services.Extensions
{
    public static class SortingExtensions
    {
        public static IEnumerable<GetProjectDTO> OrderByField(this IEnumerable<GetProjectDTO> projects, string field)
        {
            var parameters = field.Trim().Split(" ");
            
            if(parameters.Count() != 2)
            {
                return projects.OrderBy(p => p.Name);
            }    

            switch (parameters[0].ToLower())
            {
                case "startdate":
                    {
                        if (parameters[1].ToLower() == "desc")
                        {
                            projects = projects.OrderByDescending(p => p.StartDate);
                        }
                        else
                        {
                            projects = projects.OrderBy(p => p.StartDate);
                        }
                        break;
                    }
                case "enddate":
                    {
                        if (parameters[1].ToLower() == "desc")
                        {
                            projects = projects.OrderByDescending(p => p.EndDate);
                        }
                        else
                        {
                            projects = projects.OrderBy(p => p.EndDate);
                        }
                        break;
                    }
                case "priority":
                    {
                        if (parameters[1].ToLower() == "desc")
                        {
                            projects = projects.OrderByDescending(p => p.Priority);
                        }
                        else
                        {
                            projects = projects.OrderBy(p => p.Priority);
                        }
                        break;
                    }
                default:
                    {
                        if (parameters[1].ToLower() == "desc")
                        {
                            projects = projects.OrderByDescending(p => p.Name);
                        }
                        else
                        {
                            projects = projects.OrderBy(p => p.Name);
                        }
                        break;
                    }
            }

            return projects.ToList(); 
        }
    }
}
