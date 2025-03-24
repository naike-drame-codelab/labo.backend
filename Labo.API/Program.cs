using Labo.API.Configurations;
using Labo.Application.Interfaces;
using Labo.Application.Interfaces.Repositories;
using Labo.Application.Interfaces.Services;
using Labo.Application.Services;
using Labo.Infrastructure;
using Labo.Infrastructure.Repositories;
using Labo.Infrastructure.Smtp;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LaboContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("Main"))
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

app.UseAuthorization();
// app.UseAuthentication();

app.MapControllers();

app.Run();
