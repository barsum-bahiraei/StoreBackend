using Microsoft.EntityFrameworkCore;
using StoreBackend;
using StoreBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer("Server=localhost;Database=Store;User ID=sa;Password=database1234;TrustServerCertificate=True;"));

var swaggerOptions = builder.Configuration.GetSection("SwaggerOptions");
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = swaggerOptions["Title"],
        Version = swaggerOptions["Version"],
        Description = swaggerOptions["Description"],
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
