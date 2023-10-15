using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using pet_store_backend.api.Errors;
using pet_store_backend.api.Filter;
using pet_store_backend.application;
using pet_store_backend.infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddApplication()
        .AddInfrastructure(builder.Configuration);

    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();

    builder.Services.AddSingleton<ProblemDetailsFactory, PetStoreProblemDetailsFactory>();
}
var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiddleware>();

    // app.Map("/error", (HttpContext httpContext) => {
    //     Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    //     return Results.Problem(title: exception?.Message);
    // });

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
