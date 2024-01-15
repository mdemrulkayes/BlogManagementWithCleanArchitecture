using Serilog;

namespace CleanArchitecture.BlogManagement.WebApi;

public static class DependencyInjection
{
    public static ConfigureHostBuilder RegisterSerilog(this ConfigureHostBuilder builder)
    {
        builder.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(context.Configuration);
        });
        return builder;
    }
}
