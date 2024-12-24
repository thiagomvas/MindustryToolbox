using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.Entities;
public class Sector
{
    public string Name { get; set; } = "";
    public Resource Resources { get; set; } = Resource.None;
    public Threat Threat { get; set; } = Threat.Low;
    public int NumOfWaves { get; set; } = 0;
    public string[] VulnerableTo { get; set; } = [];
    public Planet Planet { get; set; } = Planet.Serpulo;
    public SectorType Type { get; set; } = SectorType.Survival;

    public override string ToString()
    {
        return $"Name: {Name}, Resources: {Resources}, Threat: {Threat}, NumOfWaves: {NumOfWaves}, VulnerableTo: {string.Join(", ", VulnerableTo)}, Planet: {Planet}";
    }
}
