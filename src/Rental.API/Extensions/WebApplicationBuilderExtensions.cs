using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Rental.API.Middlewares;
using Serilog;
using System.Text;


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

        public static void ConfigureJwt(this IServiceCollection services, string secretkey, string issuer, string audience)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(secretkey))
                };
            });
        }

    }
}
