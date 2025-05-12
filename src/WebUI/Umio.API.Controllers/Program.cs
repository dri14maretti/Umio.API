using Umio.API.Application;
using Umio.API.ViaCepService;
using Umio.API.Postgres;
using Umio.API.Controllers;
using Umio.API.Postgres.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AdicionarAplicacao()
    .AdicionarViaCepService()
    .AdicionarPostgres();

builder.Services.AddTransient<ManipuladorExcecoesApi>();

builder.Services.AddDbContext<UmioDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
