using AutoMapper;
using ProjectControlAPI.Common.DTOs.TaskDTOs;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.Common.Mapping
{
    public class TaskProfile : Profile
    {
       public TaskProfile()
        {
            CreateMap<CreateTaskDTO, TaskProject>(); 
            CreateMap<TaskProject, GetTaskDTO>();
        }
    }
}
