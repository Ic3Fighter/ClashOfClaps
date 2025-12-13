using ClashOfClaps.Presentation.Models;

namespace ClashOfClaps.Data.DataProviders;

public class CacheDataProvider
{
    public Dictionary<string, ApplauseVolume> Volumes { get; set; }

    public CacheDataProvider()
    {
        var rnd = new Random();
        Volumes ??= Enumerable.Range(0, 3)
            .Select(i => new ApplauseVolume
            {
                TeamName = $"Team{i}",
                Volume = rnd.NextDouble() * 100
            })
            .ToDictionary(x => x.TeamName, y => y);
    }
}