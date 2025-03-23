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

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

builder.Services.AddScoped(c => new SmtpClient
{
    Host = builder.Configuration["Smtp:Host"] ?? throw new Exception("Missing Smtp Configuration"),
    Port = int.Parse(builder.Configuration["Smtp:Port"] ?? throw new Exception("Missing Smtp Configuration")),
    Credentials = new NetworkCredential
    {
        UserName = builder.Configuration["Smtp:Username"] ?? throw new Exception("Missing Smtp Configuration"),
        Password = builder.Configuration["Smtp:Password"] ?? throw new Exception("Missing Smtp Configuration"),
    }

});
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

app.MapControllers();

app.Run();
