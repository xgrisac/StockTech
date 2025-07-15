using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scalar.AspNetCore; // Scalar substituindo Swagger para documentação de API
using Scalar.AspNetCore.Swashbuckle;
using System.Reflection;
using WebAPI.net9.Data;
using WebAPI.net9.Interfaces;
using WebAPI.net9.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Add services to the container. 

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi()
    .AddSwaggerGen(options =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

builder.Services.AddScoped<IAppDbContext, AppDbContext>(); // Injeta a dependência, permitindo que o contexto do banco de dados seja usado em toda a aplicação

builder.Services.AddDbContext<AppDbContext>(options => // Configuração do DbContext para usar o SQL Server
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // Obtém a string de conexão do arquivo appsettings.json
});

builder.Services.AddScoped<ProdutoService>(); // Registra o serviço ProdutoService para injeção de dependência

var app = builder.Build(); // Começo da aplicação

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(); // Mapeia referências de API para o ambiente de desenvolvimento
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
