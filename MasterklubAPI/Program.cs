using System.Text;
using System.Text.Json.Serialization;
using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces;
using Masterklub.Infrastructure.Data;
using Masterklub.Infrastructure.UnitOfWork;
using MasterklubAPI.Auth;
using MasterklubAPI.Filters;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MasterklubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MasterklubConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IProdavacService, ProdavacService>();
builder.Services.AddScoped<IProizvodService, ProizvodService>();
builder.Services.AddScoped<INagradaService, NagradaService>();
builder.Services.AddScoped<IProdajaService, ProdajaService>();
builder.Services.AddScoped<INarudzbinaNagradeService, NarudzbinaNagradeService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<IPasswordHasher<Korisnik>, PasswordHasher<Korisnik>>();
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
    ?? throw new InvalidOperationException("Jwt konfiguracija nije definisana u appsettings.json.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
