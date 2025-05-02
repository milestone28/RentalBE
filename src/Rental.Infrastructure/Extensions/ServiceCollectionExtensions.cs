

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
        public static void  AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RentalDb");

            services.AddDbContext<RentalDBContext>(options =>
                options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());
            services.AddIdentityApiEndpoints<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<RentalDBContext>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDefaultSeeders, DefaultSeeders>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();
            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
        }
    }
}
