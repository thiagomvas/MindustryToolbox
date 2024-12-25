using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.Entities;
public class ProductionRecipe
{
    public Resource Resource { get; set; }
    public Structure Structure { get; set; }
    public double OutputPerSecond { get; set; }
    public List<ProductionNode> Inputs { get; set; } = new();

    public ProductionRecipe(Resource resource, Structure structure, double outputPerSecond)
    {
        Resource = resource;
        Structure = structure;
        OutputPerSecond = outputPerSecond;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        ToStringRecursive(sb, 0);
        return sb.ToString();
    }

    internal void ToStringRecursive(StringBuilder sb, int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);
        var output = Structure.Outputs.First(o => o.Resource == Resource);
        var amountNeeded = Math.Ceiling(OutputPerSecond / output.Rate);
        sb.AppendLine($"{indent}{Structure.Name} needed: {amountNeeded} producing {Math.Round(OutputPerSecond, 4)}/s.");

        foreach (var input in Inputs)
        {
            input.ToStringRecursive(sb, indentLevel + 1);
        }
    }
}
