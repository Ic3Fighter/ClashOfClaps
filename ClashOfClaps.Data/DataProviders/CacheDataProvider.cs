using ClashOfClaps.Data.Options;
using ClashOfClaps.Presentation.Models;
using Microsoft.Extensions.Options;

namespace ClashOfClaps.Data.DataProviders;

public class CacheDataProvider
{
    public Dictionary<string, ApplauseVolume> Volumes { get; set; }
    public Dictionary<string, int> Points { get; set; }
    
    public CacheDataProvider(IOptions<ApplicationOptions> options)
    {
        var rnd = new Random();
        var teams = options.Value.Teams;
        
        Volumes = teams
            .Select(x => new ApplauseVolume
            {
                TeamName = x.Id,
                Volume = rnd.NextDouble() * 100
            })
            .ToDictionary(x => x.TeamName, y => y);

        Points = teams.ToDictionary(x => x.Id, x => x.StartingPoints);
    }
}