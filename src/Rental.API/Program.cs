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
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuthentication();


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
        c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please Insert Token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });

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
        // add Ba
        // add JWT Authentication
        //var jwtSecurityScheme = new OpenApiSecurityScheme
        //{
        //    Reference = new OpenApiReference
        //    {
        //        Type = ReferenceType.SecurityScheme,
        //        Id = JwtBearerDefaults.AuthenticationScheme
        //    },
        //    Scheme = "Bearer",
        //    BearerFormat = "JWT",
        //    Name = "OAuth 2.0 Authentication",
        //    In = ParameterLocation.Header,
        //    Type = SecuritySchemeType.Http,
        //    Description = "JWT Bearer token"
        //};
        //c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
        //c.AddSecurityRequirement(new OpenApiSecurityRequirement
        //            {
        //                { jwtSecurityScheme, new string[] { }}
        //            });

        //// add Basic Authentication
        //var basicSecurityScheme = new OpenApiSecurityScheme
        //{
        //    Type = SecuritySchemeType.Http,
        //    Scheme = "basic",
        //    Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
        //};
        //c.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
        //c.AddSecurityRequirement(new OpenApiSecurityRequirement
        //            {
        //                { basicSecurityScheme, new string[] { }}
        //            });

        //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //c.IncludeXmlComments(xmlPath);
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();
app.UseAuthorization();

app.MapControllers();

app.Run();
