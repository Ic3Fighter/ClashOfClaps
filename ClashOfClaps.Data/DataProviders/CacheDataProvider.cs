using ClashOfClaps.Presentation.Models;

namespace ClashOfClaps.Data.DataProviders;

public class CacheDataProvider
{
    public Dictionary<string, ApplauseVolume> Volumes { get; set; }

    public Dictionary<string, int> Points { get; set; }

    public CacheDataProvider()
    {
        var rnd = new Random();
        var teams = Enumerable.Range(0, 3).Select(i => $"Team{i}").ToArray();
        
        Volumes = teams
            .Select(x => new ApplauseVolume
            {
                TeamName = x,
                Volume = rnd.NextDouble() * 100
            })
            .ToDictionary(x => x.TeamName, y => y);

        Points = teams.ToDictionary(x => x, _ => 0);
    }
}