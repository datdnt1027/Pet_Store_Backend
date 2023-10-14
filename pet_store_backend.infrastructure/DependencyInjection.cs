using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Services;
using pet_store_backend.infrastructure.Authentication;
using pet_store_backend.infrastructure.Services;

namespace pet_store_backend.infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.Configure<JwtSetting>(configuration.GetSection(JwtSetting.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}
