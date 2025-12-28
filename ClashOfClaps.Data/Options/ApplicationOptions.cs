using ClashOfClaps.Data.Models;

namespace ClashOfClaps.Data.Options;

public class ApplicationOptions
{
    public int InputChannel { get; set; }

    public int SampleRate { get; set; }

    public Boundaries Boundaries { get; set; }

    public List<TeamSettings> Teams { get; set; }
}

public class Boundaries
{
    public int Low { get; set; }

    public int High { get; set; }
}