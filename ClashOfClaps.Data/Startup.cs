using ClashOfClaps.Data.DataProviders;
using Microsoft.Extensions.DependencyInjection;

namespace ClashOfClaps.Data;

public static class Startup
{
    public static void AddData(this IServiceCollection services)
    {
        services.AddSingleton<CacheDataProvider>();
    }
}