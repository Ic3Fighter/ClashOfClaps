using ClashOfClaps.Data.DataProviders;
using ClashOfClaps.Data.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClashOfClaps.Data;

public static class Startup
{
    public static void AddData(this IServiceCollection services, IConfiguration configuration)
    {
        // add data providers
        services.AddSingleton<CacheDataProvider>();
        services.AddSingleton<AudioMeterDataProvider>();

        // configure application options
        services.Configure<ApplicationOptions>(configuration.GetSection("Options"));
    }
}