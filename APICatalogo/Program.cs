using APICatalogo.Context;
using APICatalogo.Extensions;
using APICatalogo.Filters;
using APICatalogo.Logging;
using APICatalogo.Repositores;
using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using APICatalogo.DTOs.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(ApiExcepionFilter));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }).AddNewtonsoftJson();
      

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)));


builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRapository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(ProdutoDTOMappingProfile));
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();