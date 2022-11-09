using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ProjectControlAPI.Presentation.Extensions
{
    public static class AddOptionsAutorizationExtension
    {
        public static void AddOptionsAutorization(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Director", builder =>
                {
                    builder.RequireRole(ClaimTypes.Role, "Director");
                });

                options.AddPolicy("DirectorOrPM", builder =>
                {
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Director")
                                                || x.User.HasClaim(ClaimTypes.Role, "ProjectManager"));
                });

                options.AddPolicy("AllWorkers", builder =>
                {
                    builder.RequireAuthenticatedUser();
                });
            });
        }
    }
}
