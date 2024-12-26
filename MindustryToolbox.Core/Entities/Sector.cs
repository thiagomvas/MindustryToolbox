using MindustryToolbox.Core.ValueTypes;

namespace MindustryToolbox.Core.Entities;
/// <summary>
/// Represents a sector in the game.
/// </summary>
public class Sector
{
    /// <summary>
    /// Gets or sets the name of the sector.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets the resources available in the sector.
    /// </summary>
    public Resource Resources { get; set; } = Resource.None;

    /// <summary>
    /// Gets or sets the threat level of the sector.
    /// </summary>
    public Threat Threat { get; set; } = Threat.Low;

    /// <summary>
    /// Gets or sets the number of waves in the sector.
    /// </summary>
    public int NumOfWaves { get; set; } = 0;

    /// <summary>
    /// Gets or sets the vulnerabilities of the sector.
    /// </summary>
    public string[] VulnerableTo { get; set; } = [];

    /// <summary>
    /// Gets or sets the planet on which the sector is located.
    /// </summary>
    public Planet Planet { get; set; } = Planet.Serpulo;

    /// <summary>
    /// Gets or sets the type of the sector.
    /// </summary>
    public SectorType Type { get; set; } = SectorType.Survival;

    /// <summary>
    /// Returns a string that represents the current sector.
    /// </summary>
    /// <returns>A string that represents the current sector.</returns>
    public override string ToString()
    {
        return $"Name: {Name}, Resources: {Resources}, Threat: {Threat}, NumOfWaves: {NumOfWaves}, VulnerableTo: {string.Join(", ", VulnerableTo)}, Planet: {Planet}";
    }
}
