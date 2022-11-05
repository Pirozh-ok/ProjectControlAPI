using AutoMapper;
using ProjectControlAPI.Common.DTOs.ProjectDTOs;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.Common.Mapping
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, GetProjectDTO>();
            CreateMap<CreateProjectDTO, Project>();
        }
    }
}
