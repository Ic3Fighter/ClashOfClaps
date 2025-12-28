using ClashOfClaps.Data.DataProviders;
using ClashOfClaps.Data.Options;
using Microsoft.Extensions.Options;

namespace ClashOfClaps.Business.BusinessProviders;

public class CacheBusinessProvider
{
    private readonly int _lowBoundary;
    private readonly int _highBoundary;

    private readonly CacheDataProvider _cacheDataProvider;
    private readonly AudioMeterDataProvider _audioMeterDataProvider;

    public CacheBusinessProvider(IOptions<ApplicationOptions> options,
        CacheDataProvider cacheDataProvider,
        AudioMeterDataProvider audioMeterDataProvider)
    {
        _lowBoundary = options.Value.Boundaries.Low;
        _highBoundary = options.Value.Boundaries.High;

        if (_lowBoundary > 0 || _highBoundary > 0 || _lowBoundary > _highBoundary)
            throw new ArgumentException("Chosen boundaries for volume percentage conversion are invalid!");

        _cacheDataProvider = cacheDataProvider;
        _audioMeterDataProvider = audioMeterDataProvider;
    }

    private double ToPercentage(double value) =>
        value < _lowBoundary
            ? 0
            : value > _highBoundary
                ? 1
                : Math.Abs((value - _lowBoundary) / (_lowBoundary - _highBoundary));

    public void RandomizeVolumes()
    {
        var volumes = _cacheDataProvider.Volumes;

        foreach (var applauseVolume in volumes)
            volumes[applauseVolume.Key] =
                ToPercentage(_audioMeterDataProvider.RmsDbFs);

        _cacheDataProvider.Volumes = volumes;
    }

    public Dictionary<string, double> GetVolumes() => _cacheDataProvider.Volumes;

    public Dictionary<string, int> GetPoints() => _cacheDataProvider.Points;
}