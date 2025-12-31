using ClashOfClaps.Data.Models;

namespace ClashOfClaps.Data.Options;

public class ApplicationOptions
{
    /// <summary>
    /// Input channel of system default device to use for volume measurements
    /// </summary>
    public int InputChannel { get; set; }

    /// <summary>
    /// Sample rate at which the input should be measured
    /// </summary>
    public int SampleRate { get; set; }

    /// <summary>
    /// Lower and upper boundaries of input measurements
    /// </summary>
    public Boundaries Boundaries { get; set; }

    /// <summary>
    /// Amount of maximum measurements to keep in cache for each team
    /// </summary>
    public int RecentVolumeMeasurements { get; set; }

    /// <summary>
    /// Each teams configured settings
    /// </summary>
    public List<TeamSettings> Teams { get; set; }
}

public class Boundaries
{
    /// <summary>
    /// Lower boundary for audio input volume (in dbFS)
    /// </summary>
    public int Low { get; set; }

    /// <summary>
    /// Upper boundary for audio input volume (in dbFS)
    /// </summary>
    public int High { get; set; }
}