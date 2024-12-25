using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.Converters;
public class StructureTypeJsonConverter : JsonConverter<StructureType>
{
    public override StructureType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
            return StructureType.Other; // Default to "Other" if the value is missing

        if (Enum.TryParse(typeof(StructureType), value, true, out var parsedType))
        {
            return (StructureType)parsedType;
        }

        return StructureType.Other; // Default to "Other" if the value is invalid
    }

    public override void Write(Utf8JsonWriter writer, StructureType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
