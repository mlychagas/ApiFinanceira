using ApiFinanceira.DataContexts;
using ApiFinanceira.Profiles;
using ApiFinanceira.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("mysql");
builder.Services.AddDbContext<AppDbContex>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// ignorar ciclos de referência e formatar o JSON de forma legível
builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// new MySqlServerVersion(new Version(8,0,32) em vez de ServerVersion.AutoDetect(connectionString)
// para não quebrar a API

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DespesaService>();
// 
builder.Services.AddAutoMapper(config => config.AddProfile<DespesaProfile>());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
