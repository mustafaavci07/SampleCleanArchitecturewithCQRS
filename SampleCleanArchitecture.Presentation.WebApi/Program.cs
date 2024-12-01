
using SampleCleanArchitecture.Application;
using SampleCleanArchitecture.Presentation.WebApi;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

string environmentLevel = Environment.GetEnvironmentVariable("environment")??"Development";
string rootPath = Directory.GetCurrentDirectory();

IConfiguration config = builder.Configuration.AddJsonFile(Path.Combine(rootPath, $"appsettings.{environmentLevel}.json")).Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApplicationMethods(config);


var app = builder.Build();
app.RegisterAPIs();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
