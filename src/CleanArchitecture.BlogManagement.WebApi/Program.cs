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

app.UseHttpsRedirection();

app.Run();
