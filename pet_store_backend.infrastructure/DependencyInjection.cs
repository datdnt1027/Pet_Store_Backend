using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Email;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Common.Interfaces.Services;
using pet_store_backend.infrastructure.Authentication;
using pet_store_backend.infrastructure.Email;
using pet_store_backend.infrastructure.Persistence;
using pet_store_backend.infrastructure.Persistence.Repositories;
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
            services.Configure<EmailSetting>(configuration.GetSection(EmailSetting.SectionName));

            services
                .AddAuth(configuration)
                .AddPersistence();

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IEmailService, EmailSending>();

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer("Data Source=.;Initial Catalog=PetStore;Trusted_Connection=True;Encrypt=false"));

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IPasswordConfiguration, PasswordConfiguration>();
            var jwtSetting = new JwtSetting();
            configuration.Bind(JwtSetting.SectionName, jwtSetting);
            services.AddSingleton(Options.Create(jwtSetting));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting.Issuer,
                    ValidAudience = jwtSetting.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey))
                });

            return services;
        }
    }
}
