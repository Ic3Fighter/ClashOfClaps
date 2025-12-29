using ClashOfClaps.Data.Framework;
using ClashOfClaps.Data.Models;
using ClashOfClaps.Data.Options;
using Microsoft.Extensions.Options;

namespace ClashOfClaps.Data.DataProviders;

public class CacheDataProvider
{
    private readonly ApplicationOptions _options;

    public Dictionary<string, ApplauseVolume> Volumes { get; private set; }

    public Dictionary<string, int> Points { get; private set; }

    public CacheDataProvider(IOptions<ApplicationOptions> options)
    {
        _options = options.Value;

        InitVolumes();
        InitPoints();
    }

    /// <summary>
    /// Initialize/reset points dictionary
    /// </summary>
    private void InitPoints() => Points = _options.Teams.ToDictionary(x => x.Id, x => x.StartingPoints);

    /// <summary>
    /// Initialize/reset volumes dictionary
    /// </summary>
    private void InitVolumes() => Volumes = _options.Teams.ToDictionary(x => x.Id, _ => new ApplauseVolume
    {
        RecentVolumes = new LimitedQueue<double>(_options.RecentVolumeMeasurements),
    });
}