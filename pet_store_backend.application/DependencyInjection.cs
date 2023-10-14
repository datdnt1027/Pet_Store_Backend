using Microsoft.Extensions.DependencyInjection;
using pet_store_backend.application.Services.Authentication;

namespace pet_store_backend.application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
