using Umio.API.Application;
using Umio.API.ViaCepService;
using Umio.API.Postgres;
using Umio.API.Controllers;
using Umio.API.Postgres.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Umio.API.TokenService.Models;
using Umio.API.TokenService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfiguracoesJwt jwtSettings = new ConfiguracoesJwt();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key)),
    };
});


builder.Services
    .AdicionarAplicacao()
    .AdicionarViaCepService()
    .AdicionarPostgres()
    .AdicionarTokenService(jwtSettings);

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

app.UseMiddleware<ManipuladorExcecoesApi>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
