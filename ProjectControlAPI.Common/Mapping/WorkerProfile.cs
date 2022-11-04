using AutoMapper;
using ProjectControlAPI.Common.DTOs;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.Common.Mapping
{
    public class WorkerProfile : Profile
    {
        public WorkerProfile()
        {
            CreateMap<Worker, GetWorkerDTO>();
            CreateMap<CreateWorkerDTO, Worker>(); 
        }
    }
}
