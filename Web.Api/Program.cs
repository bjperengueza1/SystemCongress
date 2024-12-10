using Application.Automappers;
using Application.Congress.Interfaces;
using Application.Congress.Services;
using Application.Exposures.Interfaces;
using Application.Exposures.Services;
using Application.Files;
using Application.Files.Interfaces;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.7

// Configuración de la cadena de conexión a la base de datos
builder.Services.AddDbContext<CongressContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("StoreConnectionMysql");
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ICongressRepository, CongressRepository>(); // Asegúrate de que CongresoRepository implemente ICongresoRepository
builder.Services.AddScoped<ICongressService, CongressService>();

builder.Services.AddScoped<IExposureRepository, ExposureRepository>(); // Asegúrate de que ExposureRepository implemente IExposureRepository
builder.Services.AddScoped<IExposureService, ExposureService>();

builder.Services.AddSingleton<IFileStorageService, FileStorageService>();
builder.Services.AddTransient<IFileService, FileService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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