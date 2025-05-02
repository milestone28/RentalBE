using Rental.API.Extensions;
using Rental.API.Middlewares;
using Rental.Application.Extensions;
using Rental.Domain.Entities;
using Rental.Infrastructure.Extensions;
using Rental.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

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
