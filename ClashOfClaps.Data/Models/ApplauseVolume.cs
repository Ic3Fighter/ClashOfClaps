using ClashOfClaps.Data.Framework;
using ClashOfClaps.Data.Options;

namespace ClashOfClaps.Data.Models;

public class ApplauseVolume
{
    /// <summary>
    /// Recently measured volume values for this team
    /// </summary>
    /// <remarks>Length of <see cref="LimitedQueue{T}"/> can be configured in <see cref="ApplicationOptions"/></remarks>
    public LimitedQueue<double> RecentVolumes { get; set; }

    /// <summary>
    /// Overall measured peak value for this team
    /// </summary>
    public double Peak { get; set; } = int.MinValue;

    /// <summary>
    /// Set whether new measurements can be accepted for this team
    /// </summary>
    public bool IsActive { get; set; } = false;
}