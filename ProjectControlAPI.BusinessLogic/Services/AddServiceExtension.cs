using Microsoft.Extensions.DependencyInjection;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.BusinessLogic.Services.Interfaces;

namespace ProjectControlAPI.BusinessLogic.Services
{
    public static class AddServiceExtension
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IWorkerService, WorkerService>(); 
        }
    }
}
