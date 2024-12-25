using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.Converters;
public class ResourceJsonConverter : JsonConverter<Resource>
{
    public override Resource Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
            return Resource.None;

        var resources = value.Split(", ", StringSplitOptions.RemoveEmptyEntries);
        Resource result = Resource.None;

        foreach (var resource in resources)
        {
            if (Enum.TryParse(typeof(Resource), resource, true, out var parsedResource))
            {
                result |= (Resource)parsedResource;
            }
            else
            {
                throw new JsonException($"Invalid resource value: {resource}");
            }
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, Resource value, JsonSerializerOptions options)
    {
        var resourceNames = Enum.GetValues(typeof(Resource))
            .Cast<Resource>()
            .Where(r => r != Resource.None && value.HasFlag(r))
            .Select(r => r.ToString());

        writer.WriteStringValue(string.Join(", ", resourceNames));
    }
}

