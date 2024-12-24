using MindustryToolbox.Core;
using MindustryToolbox.Core.ValueTypes;

var context = new MindustryDbContext();

var sectors = context.Sectors.ToArray();

// Ensure sectors have matching wave counts
foreach (var sector in sectors)
{
    bool hasCorrectNumOfWaves = sector.Threat switch
    {
        Threat.Low => sector.NumOfWaves is 20 or -1,
        Threat.Medium => sector.NumOfWaves is 25 or -1,
        Threat.High => sector.NumOfWaves is 35 or 45 or -1,
        Threat.Extreme => sector.NumOfWaves is 55 or -1,
        Threat.Eradication => sector.NumOfWaves is 65 or -1,
    };

    if (sector.Type == SectorType.Attack)
        hasCorrectNumOfWaves = true;

    if (!hasCorrectNumOfWaves)
    {
        Console.WriteLine($"Sector {sector.Name} has an incorrect number of waves for its threat level.");
    }
}