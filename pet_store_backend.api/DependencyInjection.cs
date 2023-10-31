using Microsoft.AspNetCore.Mvc.Infrastructure;
using pet_store_backend.api.Common.Errors;
using pet_store_backend.api.Common.Mapping;

namespace pet_store_backend.api;


public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, PetStoreProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}