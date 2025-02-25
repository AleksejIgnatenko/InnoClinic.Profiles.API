using System.Text;
using InnoClinic.Profiles.API.Middlewares;
using InnoClinic.Profiles.Application.MapperProfiles;
using InnoClinic.Profiles.Application.RabbitMQ;
using InnoClinic.Profiles.Application.Services;
using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.DataAccess.Context;
using InnoClinic.Profiles.DataAccess.Repositories;
using InnoClinic.Profiles.Infrastructure.Jwt;
using InnoClinic.Profiles.Infrastructure.RabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InnoClinicProfilesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<RabbitMQSetting>(
    builder.Configuration.GetSection("RabbitMQ"));

// Load JWT settings
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));

var jwtOptions = builder.Configuration.GetSection("JwtSettings").Get<JwtOptions>();

// Add JWT bearer authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions?.SecretKey))
    };
});

builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IOfficeRepository, OfficeRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();

builder.Services.AddHostedService<RabbitMQListener>();

builder.Services.AddAutoMapper(typeof(MapperProfiles));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var rabbitMQService = services.GetRequiredService<IRabbitMQService>();
    await rabbitMQService.CreateQueuesAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors(policy => policy
    .WithOrigins("http://localhost:4000") // Разрешенный источник запросов
    .AllowAnyMethod() // Разрешить любые HTTP методы
    .AllowAnyHeader() // Разрешить любые заголовки HTTP
);

app.Run();
