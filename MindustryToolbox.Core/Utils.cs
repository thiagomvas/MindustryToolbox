using MindustryToolbox.Core.Converters;
using MindustryToolbox.Core.DTOs;
using MindustryToolbox.Core.Entities;
using MindustryToolbox.Core.ValueTypes;
using System.Text.Json;

namespace MindustryToolbox.Core;

internal static class Utils
{

    public static IEnumerable<Structure> ParseStructures(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return ParseStructuresFromText(json);
    }
    public static IEnumerable<Structure> ParseStructuresFromText(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new ResourceJsonConverter(),
                new StructureTypeJsonConverter()
            }
        };
        return JsonSerializer.Deserialize<IEnumerable<Structure>>(json, options);
    }
    public static List<Sector> ParseSectors(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return ParseSectorsFromJson(json);
    }
    public static List<Sector> ParseSectorsFromJson(string json)
    {
        // Deserialize into DTOs
        var sectors = JsonSerializer.Deserialize<IEnumerable<SectorDTO>>(json);


        // Convert DTOs to entities
        return sectors.Select(Sector.FromDto).ToList();

    }

    public static Threat ParseThreat(string threatLevel)
    {
        return threatLevel.ToUpper() switch
        {
            "LOW" => Threat.Low,
            "MEDIUM" => Threat.Medium,
            "HIGH" => Threat.High,
            "EXTREME" => Threat.Extreme,
            "ERADICATION" => Threat.Eradication,
            _ => Threat.Low
        };
    }

    public static Resource ParseResources(string[] parts)
    {
        Resource resources = Resource.None;

        // Map columns (3-11) to Resource flags (ensure parts are within range)
        if (parts.Length >= 12)
        {
            if (!string.IsNullOrWhiteSpace(parts[3])) resources |= Resource.Copper;
            if (!string.IsNullOrWhiteSpace(parts[4])) resources |= Resource.Lead;
            if (!string.IsNullOrWhiteSpace(parts[5])) resources |= Resource.Sand;
            if (!string.IsNullOrWhiteSpace(parts[6])) resources |= Resource.Coal;
            if (!string.IsNullOrWhiteSpace(parts[7])) resources |= Resource.Titanium;
            if (!string.IsNullOrWhiteSpace(parts[8])) resources |= Resource.Thorium;
            if (!string.IsNullOrWhiteSpace(parts[9])) resources |= Resource.Scrap;
            if (!string.IsNullOrWhiteSpace(parts[10])) resources |= Resource.Water;
            if (!string.IsNullOrWhiteSpace(parts[11])) resources |= Resource.Oil;
            if (!string.IsNullOrWhiteSpace(parts[12])) resources |= Resource.Magma;
        }

        return resources;
    }

    public static string[] ParseVulnerableTo(string vulnerableTo)
    {
        if (string.IsNullOrWhiteSpace(vulnerableTo) || vulnerableTo == "None") return Array.Empty<string>();

        // Split by comma and ampersand, then trim each element
        return vulnerableTo.Split(new[] { ',', '&' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(v => v.Trim())
                           .ToArray();
    }
}
