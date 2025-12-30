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
    private void InitVolumes() => Volumes = Volumes?.ToDictionary( // try to reset to existing dictionary
        x => x.Key,
        y => new ApplauseVolume
        {
            RecentVolumes = new LimitedQueue<double>(_options.RecentVolumeMeasurements),
            IsActive = y.Value.IsActive,
        })
        ?? _options.Teams.ToDictionary( // create a new dictionary from ApplicationOptions
            x => x.Id,
            _ => new ApplauseVolume
            {
                RecentVolumes = new LimitedQueue<double>(_options.RecentVolumeMeasurements),
                IsActive = true,
            });

    /// <summary>
    /// Reset volume measurements
    /// </summary>
    public void ResetVolumes() => InitVolumes();

    /// <summary>
    /// Set a team to active based on its unique id.
    /// This will reset all other teams to inactive.
    /// </summary>
    public void SetActive(string teamId, bool setActive)
    {
        foreach (var applauseVolume in Volumes)
            applauseVolume.Value.IsActive = applauseVolume.Key == teamId && setActive;
    }

    /// <summary>
    /// Get the currently active team
    /// </summary>
    public string GetActive() =>
        Volumes.First(x => x.Value.IsActive).Key;
}