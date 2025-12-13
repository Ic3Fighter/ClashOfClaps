using ClashOfClaps.Business.BusinessProviders;
using Microsoft.Extensions.DependencyInjection;

namespace ClashOfClaps.Business;

public static class Startup
{
    public static void AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<CacheBusinessProvider>();
        services.AddScoped<PointsBusinessProvider>();
    }
}