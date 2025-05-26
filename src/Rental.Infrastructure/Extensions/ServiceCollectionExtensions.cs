

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.Configuration;
using Rental.Infrastructure.Persistence;
using Rental.Infrastructure.Repositories;
using Rental.Infrastructure.Seeders;
using Rental.Infrastructure.Storage;

namespace Rental.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RentalDb");
            //services.AddIdentityApiEndpoints<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDBContext>();

            //using mssql
            //services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());
            //services.AddDbContext<AuthDBContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

            //using mysql
            services.AddDbContext<AppDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).EnableSensitiveDataLogging());
            services.AddDbContext<AuthDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).EnableSensitiveDataLogging());
            services.AddIdentityCore<User>().AddRoles<IdentityRole>().AddTokenProvider<DataProtectorTokenProvider<User>>("Rental").AddEntityFrameworkStores<AuthDBContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
             {
                 options.User.RequireUniqueEmail = true;
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 6;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequiredUniqueChars = 1;
             });
            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDefaultSeeders, DefaultSeeders>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
        }
    }
}
