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

    /// <summary>
    /// Convert raw volume or peak value to a percentage from 0 to 1 between low and high boundaries defined in <see cref="ApplicationOptions"/>.
    /// Either <paramref name="value"/> or <paramref name="peak"/> will be chosen, whichever is higher.
    /// </summary>
    private double ToPercentage(double value, double peak) =>
        value < _lowBoundary && peak < _lowBoundary
            ? 0
            : value > _highBoundary || peak > _highBoundary
                ? 1
                : Math.Abs((Math.Max(value, peak) - _lowBoundary) / (_lowBoundary - _highBoundary));

    /// <summary>
    /// Measure current volume from system default input device and save it in queue
    /// </summary>
    public void MeasureVolumes()
    {
        var volumes = _cacheDataProvider.Volumes;

        foreach (var applauseVolume in volumes)
        {
            var team = volumes[applauseVolume.Key];
            var volume = _audioMeterDataProvider.RmsDbFs;

            // add new volume measurement
            team.RecentVolumes.Enqueue(volume);

            // check if current volume is higher than peak and replace it in that case
            if (volume > team.Peak)
                team.Peak = volume;
        }
    }

    /// <summary>
    /// Get a dictionary with each team's id as key and a percentage value from 0 to 1 (incl.) representing the last known volume
    /// </summary>
    public Dictionary<string, double> GetVolumes() => _cacheDataProvider.Volumes.ToDictionary(
        x => x.Key,
        y => ToPercentage(y.Value.RecentVolumes.DefaultIfEmpty(double.MinValue).Max(), y.Value.Peak));

    /// <summary>
    /// Get dictionary with each team's id as key and points as value
    /// </summary>
    public Dictionary<string, int> GetPoints() => _cacheDataProvider.Points;
}