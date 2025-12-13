using System.Numerics;
using ClashOfClaps.Data.DataProviders;
using ClashOfClaps.Presentation.Models;

namespace ClashOfClaps.Business.BusinessProviders;

public class CacheBusinessProvider
{
    private readonly CacheDataProvider _cacheDataProvider;

    public CacheBusinessProvider(CacheDataProvider cacheDataProvider)
    {
        _cacheDataProvider = cacheDataProvider;
    }

    private static T Clamp<T>(T value, T low, T high) where T : INumber<T>
    {
        if (value < low) return low;
        if (value > high) return high;
        return value;
    }

    public void RandomizeVolumes()
    {
        var rnd = new Random();
        var volumes = _cacheDataProvider.Volumes;
        foreach (var applauseVolume in volumes.Values)
            applauseVolume.Volume = Clamp(applauseVolume.Volume + 2 * Math.Pow(rnd.NextDouble() - .5, 3) * 100, 0, 100);

        _cacheDataProvider.Volumes = volumes;
    }

    public Dictionary<string, ApplauseVolume> GetVolumes() => _cacheDataProvider.Volumes;

    public Dictionary<string, int> GetPoints() => _cacheDataProvider.Points;
}