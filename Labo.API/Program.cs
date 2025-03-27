using System.Text;
using Labo.API.Configurations;
using Labo.API.Security;
using Labo.Application.Interfaces;
using Labo.Application.Interfaces.Security;
using Labo.Infrastructure;
using Labo.Infrastructure.Smtp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LaboContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("Main"))
);

TokenManager.Config config = builder.Configuration.GetSection("Jwt").Get<TokenManager.Config>() ?? throw new Exception("Missing Jwt config.");
builder.Services.AddSingleton<ITokenManager, TokenManager>(
    _ => new TokenManager(config)
 );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
        o => o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = config.Issuer,
            ValidateAudience = true,
            ValidAudience = config.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret))
        }
    );

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddSmtp(builder.Configuration);
builder.Services.AddScoped<IMailer, Mailer>();

builder.Services.AddCors(b => b.AddDefaultPolicy(o =>
{
    o.AllowAnyMethod();
    o.AllowAnyOrigin();
    o.AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
