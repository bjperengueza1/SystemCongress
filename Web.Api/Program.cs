using Application.Utils.Automappers;
using Application.Congresses.Interfaces;
using Application.Congresses.Services;
using Application.Exposures.Interfaces;
using Application.Exposures.Services;
using Application.Files;
using Application.Files.Interfaces;
using Application.Password;
using Application.Rooms.Interfaces;
using Application.Rooms.Services;
using Application.Token;
using Application.Users.Interfaces;
using Application.Users.Services;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("http://localhost:4200",
                "http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.7

// Configuración de la cadena de conexión a la base de datos
builder.Services.AddDbContext<CongressContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("StoreConnectionMysql");
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IUserRepository, UserRepository>(); // Asegúrate de que CongresoRepository implemente ICongresoRepository
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICongressRepository, CongressRepository>(); // Asegúrate de que CongresoRepository implemente ICongresoRepository
builder.Services.AddScoped<ICongressService, CongressService>();

builder.Services.AddScoped<IRoomRepository, RoomRespository>(); // Asegúrate de que CongresoRepository implemente ICongresoRepository
builder.Services.AddScoped<IRoomService, RoomService>();

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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.Use(async (context, next) =>
{
    
    /*context.Request.EnableBuffering();

    // Leer el cuerpo de la solicitud
    using (var reader = new StreamReader(
               context.Request.Body,
               encoding: System.Text.Encoding.UTF8,
               detectEncodingFromByteOrderMarks: false,
               bufferSize: 1024,
               leaveOpen: true))
    {
        var body = await reader.ReadToEndAsync();

        // Do some processing with body…
        Console.WriteLine(body);
    }*/
    
    await next.Invoke();
});

app.MapControllers();

app.Run();