using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.DTOs;
internal class SectorDTO
{
    /// <summary>
    /// Gets or sets the name of the sector.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets the resources available in the sector.
    /// </summary>
    public NaturalResources Resources { get; set; } = new();

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
}
