using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Rental.API.Extensions;
using Rental.API.Filter;
using Rental.API.Middlewares;
using Rental.Application.Extensions;
using Rental.Domain.Entities;
using Rental.Infrastructure.Extensions;
using Rental.Infrastructure.Seeders;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuthentication();

builder.Services.ConfigureJwt(
              builder.Configuration["JwtValues:secretkey"]!,
              builder.Configuration["JwtValues:issuer"]!,
              builder.Configuration["JwtValues:audience"]!
              );

// If using IIS:
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

if (builder.Configuration["HostConfig:Env"] != "PROD")
{
    // dev only
    builder.Services.ConfigureCors();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Rental API",
            Version = "v1",
            Description = "API for Rental ASP.NET Core v8",
            Contact = new OpenApiContact
            {
                Name = "Gary Yu",
                Email = "garzdev89@gmail.com"
            }
        });
        c.OperationFilter<AddHeaderOperationFilter>();

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference {  Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                                },
                                []
                        }
                    });

        // add JWT Authentication
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            },
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Name = "OAuth 2.0 Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "JWT Bearer token"
        };
        c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, new string[] { }}
                    });
    });

}
else
{
    // prod allow only whitelisted origins
    builder.Services.ConfigureCors(builder.Configuration.GetSection("JwtValues:allowedorigin").Get<List<string>>()!.ToArray());
}


var app = builder.Build();


var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IDefaultSeeders>();
await seeder.SeedAsync();

app.UseSerilogRequestLogging();
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
    app.UseHsts();

if (builder.Configuration["HostConfig:Env"] != "PROD")
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Enable cors
    app.UseCors("CorsPolicy");
}
else
{
    // Enable cors with origin
    app.UseCors("CorsPolicywithOrigin");
}

app.UseHttpsRedirection();
//app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
