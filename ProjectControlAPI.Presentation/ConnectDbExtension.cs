using Microsoft.EntityFrameworkCore;

namespace ProjectControlAPI.DataAccess
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
