using CleanArchitecture.BlogManagement.Application;
using CleanArchitecture.BlogManagement.Infrastructure;
using CleanArchitecture.BlogManagement.WebApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();
builder.Services.RegisterApplication().RegisterInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/weather", (ILogger<Program> logger) =>
{
    logger.LogWarning("Checking log message in Seq: Initial GET request called. {Date}", DateTime.Now);
    Results.Ok();
}).WithName("GetWeather").WithOpenApi();

app.UseHttpsRedirection();

app.Run();

public partial class Program
{

}
