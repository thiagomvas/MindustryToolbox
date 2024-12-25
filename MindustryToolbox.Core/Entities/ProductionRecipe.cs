using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.Entities;
public class ProductionRecipe
{
    public ProductionNode Parent { get; init; }
    public Structure Structure { get; init; }
    public double OutputPerSecond { get; init; }
    public List<ProductionNode> Inputs { get; set; } = new();
    public readonly int AmountNeeded;

    public ProductionRecipe(ProductionNode parent, Structure structure, double outputPerSecond)
    {
        Parent = parent;
        Structure = structure;
        OutputPerSecond = outputPerSecond;

        var output = structure.Outputs.First(o => o.Resource == parent.Resource);
        AmountNeeded = (int)Math.Ceiling(outputPerSecond / output.Rate);
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
        sb.AppendLine($"{indent}{Structure.Name} needed: {AmountNeeded} producing {Math.Round(OutputPerSecond, 4)}/s.");

        foreach (var input in Inputs)
        {
            input.ToStringRecursive(sb, indentLevel + 1);
        }
    }
}
