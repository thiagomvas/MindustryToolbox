using MindustryToolbox.Core.Converters;
using MindustryToolbox.Core.ValueTypes;
using System.Text.Json;

namespace MindustryToolbox.Core.Entities;
/// <summary>
/// Represents a structure in the Mindustry Toolbox.
/// </summary>
public class Structure
{
    /// <summary>
    /// Gets the name of the structure.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the input resource rates for the structure.
    /// </summary>
    public ResourceRate[] Inputs { get; init; } = [];

    /// <summary>
    /// Gets the output resource rates for the structure.
    /// </summary>
    public ResourceRate[] Outputs { get; init; } = [];

    /// <summary>
    /// Gets the type of the structure.
    /// </summary>
    public StructureType Type { get; init; } = StructureType.Other;

    /// <summary>
    /// Gets the liquid buffs associated with the structure.
    /// </summary>
    public LiquidBuff[] LiquidBuffs { get; init; } = [];

    /// <summary>
    /// Gets or sets the power usage of the structure.
    /// </summary>
    public double PowerUsage = 0;

    /// <summary>
    /// Deserializes a JSON string to a <see cref="Structure"/> object.
    /// </summary>
    /// <param name="json">The JSON string representing the structure.</param>
    /// <returns>A <see cref="Structure"/> object.</returns>
    public static Structure FromJson(string json)
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

        return JsonSerializer.Deserialize<Structure>(json, options);
    }
}
