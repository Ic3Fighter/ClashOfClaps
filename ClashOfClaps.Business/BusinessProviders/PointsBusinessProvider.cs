using ClashOfClaps.Data.DataProviders;

namespace ClashOfClaps.Business.BusinessProviders;

public class PointsBusinessProvider
{
    private readonly CacheDataProvider _cacheDataProvider;

    public PointsBusinessProvider(CacheDataProvider cacheDataProvider)
    {
        _cacheDataProvider = cacheDataProvider;
    }

    public bool Set(string key, int points)
    {
        if (!_cacheDataProvider.Points.ContainsKey(key)) return false;

        _cacheDataProvider.Points[key] = Math.Max(points, 0);
        return true;
    }

    public bool Change(string key, int delta)
    {
        if (!_cacheDataProvider.Points.TryGetValue(key, out var value)) return false;

        _cacheDataProvider.Points[key] = Math.Max(value + delta, 0);
        return true;
    }
}
