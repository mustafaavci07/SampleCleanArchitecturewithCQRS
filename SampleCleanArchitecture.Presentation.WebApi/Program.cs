using SampleCleanArchitecture.Application;
using SampleCleanArchitecture.Infrastructure.Persistence;

using System;

var builder = WebApplication.CreateBuilder(args);

string environmentLevel = Environment.GetEnvironmentVariable("environment")??"Development";
string rootPath = Directory.GetCurrentDirectory();

IConfiguration config = builder.Configuration.AddJsonFile(Path.Combine(rootPath, $"appsettings.{environmentLevel}.json")).Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApplicationMethods(config);

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
