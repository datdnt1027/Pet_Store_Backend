using pet_store_backend.api;
using pet_store_backend.application;
using pet_store_backend.infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
    {
        build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
        build.WithOrigins("https://test-payment.momo.vn").AllowAnyMethod().AllowAnyHeader();
    }));

}
var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiddleware>();

    // app.Map("/error", (HttpContext httpContext) => {
    //     Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    //     return Results.Problem(title: exception?.Message);
    // });
    app.UseResponseCompression();

    app.UseExceptionHandler("/error");

    app.UseCors("corspolicy");

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
