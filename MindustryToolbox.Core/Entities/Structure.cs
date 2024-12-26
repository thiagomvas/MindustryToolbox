using MindustryToolbox.Core.Converters;
using MindustryToolbox.Core.ValueTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MindustryToolbox.Core.Entities;
public class Structure
{
    public string Name { get; init; }
    public ResourceRate[] Inputs { get; init; } = [];
    public ResourceRate[] Outputs { get; init; } = [];
    public StructureType Type { get; init; } = StructureType.Other;
    public LiquidBuff[] LiquidBuffs { get; init; } = [];
    public double PowerUsage = 0;


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
