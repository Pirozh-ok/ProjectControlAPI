using Microsoft.EntityFrameworkCore;
using ProjectControlAPI.DataAccess;

namespace ProjectControlAPI.Presentation.Extensions
{
    public static class ConnectDbExtension
    {
        public static void AddDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
