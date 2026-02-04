using Asp.Versioning;
using CleanArchitecture.BlogManagement.Application;
using CleanArchitecture.BlogManagement.Application.Common.Exceptions;
using CleanArchitecture.BlogManagement.Infrastructure;
using CleanArchitecture.BlogManagement.WebApi;
using CleanArchitecture.BlogManagement.WebApi.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.RegisterSerilog();
builder.Services
    .RegisterApplication()
    .RegisterInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);

builder.Services.RegisterOpenApi();

builder.Services.AddMemoryCache();

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1);
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;

    opt.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
    opt.ApiVersionSelector =
        new CurrentImplementationApiVersionSelector(opt);
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.MapScalarApiReference();

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseMiddleware<ApiVersionHeaderValidationMiddleware>();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.MigrateDatabase();
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

app.Run();