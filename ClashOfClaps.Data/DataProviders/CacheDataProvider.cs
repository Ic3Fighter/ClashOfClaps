using ClashOfClaps.Data.Options;
using Microsoft.Extensions.Options;

namespace ClashOfClaps.Data.DataProviders;

public class CacheDataProvider
{
    public Dictionary<string, double> Volumes { get; set; }
    public Dictionary<string, int> Points { get; set; }
    
    public CacheDataProvider(IOptions<ApplicationOptions> options)
    {
        var rnd = new Random();
        var teams = options.Value.Teams;
        
        Volumes = teams.ToDictionary(x => x.Id, _ => rnd.NextDouble() * 100);
        Points = teams.ToDictionary(x => x.Id, x => x.StartingPoints);
    }
}