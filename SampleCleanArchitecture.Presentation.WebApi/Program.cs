
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

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


//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Environment.ApplicationName, Version = "v1" });
});


builder.Services.RegisterApplicationMethods(config,Log.Logger);
builder.Services.AddHttpClient();
builder.Services.AddAuthorization();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1");
    //});

    app.UseSwaggerUI();
}

app.MapGet("/api/test", () => "Test endpoint")
   .WithName("TestEndpoint")
   .WithTags("Test");
app.RegisterAPIs(app.Services.CreateScope().ServiceProvider.GetRequiredService<ISender>());
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");*/

app.UseHttpsRedirection();

app.UseAuthorization();


//app.UseHealthChecks("/health");
//app.MapControllers();

app.Run();
