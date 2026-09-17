using Masterklub.Domain.Interfaces;
using Masterklub.Infrastructure.Data;
using Masterklub.Infrastructure.UnitOfWork;
using MasterklubAPI.Filters;
using MasterklubAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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

app.UseAuthorization();

app.MapControllers();

app.Run();
