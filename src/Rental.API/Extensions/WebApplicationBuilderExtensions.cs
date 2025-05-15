using Rental.API.Middlewares;
using Serilog;


namespace Rental.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureCors(this IServiceCollection services, string[] allowedorigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicywithOrigin",
                    builder => builder.WithOrigins(allowedorigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

    }
}
