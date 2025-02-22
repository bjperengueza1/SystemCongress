using Application.Attendances.Interfaces;
using Application.Attendances.Services;
using Application.Attendees.Interfaces;
using Application.Attendees.Services;
using Application.Common;
using Application.Congresses.DTOs;
using Application.Utils.Automappers;
using Application.Congresses.Interfaces;
using Application.Congresses.Services;
using Application.Congresses.Validators;
using Application.Exposures.DTOs;
using Application.Exposures.Interfaces;
using Application.Exposures.Services;
using Application.Exposures.Validators;
using Application.Files;
using Application.Files.Interfaces;
using Application.Password;
using Application.Rooms.Interfaces;
using Application.Rooms.Services;
using Application.Token;
using Application.Users.Interfaces;
using Application.Users.Services;
using Domain.Interfaces;
using Domain.Interfaces.Files;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Email;
using Infrastructure.Files;
using Infrastructure.Repositories;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:4200",
                    "http://34.173.148.212:4200",
                    "http://34.173.148.212",
                    "http://localhost",
                    "https://cilai.istla-sigala.edu.ec")
                  .AllowAnyMethod() // Permite cualquier método (GET, POST, PUT, DELETE)
                  .AllowAnyHeader() // Permite cualquier header
                  .AllowCredentials(); // Permite cookies o autenticación con credenciales
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

builder.Services.AddSingleton<ITokenSettings, TokenSettings>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IUserRepository, UserRepository>(); // Asegúrate de que CongresoRepository implemente ICongresoRepository
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICongressRepository, CongressRepository>(); // Asegúrate de que CongresoRepository implemente ICongresoRepository
builder.Services.AddScoped<ICongressService, CongressService>();

builder.Services.AddScoped<IRoomRepository, RoomRespository>(); // Asegúrate de que CongresoRepository implemente ICongresoRepository
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<IExposureRepository, ExposureRepository>(); // Asegúrate de que ExposureRepository implemente IExposureRepository
builder.Services.AddScoped<IExposureService, ExposureService>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>(); // Asegúrate de que AuthorRepository implemente IAuthorRepository

builder.Services.AddScoped<IAttendeeRepository, AttendeeRepository>(); // Asegúrate de que AttendeeRepository implemente IAttendeeRepository
builder.Services.AddScoped<IAttendeeService, AttendeeService>();

builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>(); // Asegúrate de que AttendanceRepository implemente IAttendanceRepository
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

// Configuración de FileStorageSettings
builder.Services.Configure<FileStorageSettings>(builder.Configuration.GetSection("FileStorageSettings"));
builder.Services.AddSingleton<IFileStorageService, LocalFileStorageService>();// aqui por ejemplo puedo cambiar a AzureFileStorageService si quiero y no afecta el resto del codigo
                                                                              // porque la interfaz es la misma y la implementacion es diferente 
builder.Services.AddTransient<IFileService, FileService>();

builder.Services.Configure<GmailOptions>(
    builder.Configuration.GetSection(GmailOptions.GmailOptionsKey));

builder.Services.AddTransient<IEmailService, GmailService>();

builder.Services.AddScoped<IValidator<CongressInsertDto>, CongressInsertValidator>();
builder.Services.AddScoped<IValidator<CongressUpdateDto>, CongressUpdateValidator>();

builder.Services.AddScoped<IValidator<ExposureInsertDto>, ExposureInsertValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();