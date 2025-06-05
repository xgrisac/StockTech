using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPI.net9.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => // Configura��o do DbContext para usar o SQL Server
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // Obt�m a string de conex�o do arquivo appsettings.json
}); 

var app = builder.Build(); // Come�o da aplica��o

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
