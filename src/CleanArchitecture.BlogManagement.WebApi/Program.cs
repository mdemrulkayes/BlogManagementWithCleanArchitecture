using CleanArchitecture.BlogManagement.Application;
using CleanArchitecture.BlogManagement.Application.Common.Exceptions;
using CleanArchitecture.BlogManagement.Infrastructure;
using CleanArchitecture.BlogManagement.WebApi;
using CleanArchitecture.BlogManagement.WebApi.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();
builder.Services
    .RegisterApplication()
    .RegisterInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.RegisterSwagger();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.Map("/", () => Results.Redirect("/api"));
app.MapEndpoints();

app.Run();