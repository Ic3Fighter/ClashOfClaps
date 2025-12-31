namespace ClashOfClaps.Data.Models;

public class TeamSettings
{
    /// <summary>
    /// Unique team id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Team name as free text
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Hex code for team colors.
    /// </summary>
    /// <remarks>Can, but must not start with a #. Will be sanitized.</remarks>
    public string Color
    {
        get => field.StartsWith('#') ? field : $"#{field}";
        set;
    }

    /// <summary>
    /// Amount of points that a team starts with
    /// </summary>
    public int StartingPoints { get; set; }

    /// <summary>
    /// Path to where the team image is saved
    /// </summary>
    public string Image { get; set; }
}