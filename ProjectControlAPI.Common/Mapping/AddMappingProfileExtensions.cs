using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectControlAPI.Common.Mapping
{
    public static class AddMappingProfileExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WorkerProfile());
                mc.AddProfile(new ProjectProfile());
                mc.AddProfile(new TaskProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
