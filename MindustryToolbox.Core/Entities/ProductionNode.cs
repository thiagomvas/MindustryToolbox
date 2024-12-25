using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.Entities;
public class ProductionNode
{
    public Resource Resource { get; set; }
    public double OutputPerSecond { get; set; }
    public List<ProductionRecipe> Recipes { get; set; } = new();

    public ProductionNode(Resource resource, double outputPerSecond)
    {
        Resource = resource;
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
        sb.AppendLine($"{indent}{Resource} production needed: {OutputPerSecond}/s");

        foreach (var recipe in Recipes)
        {
            recipe.ToStringRecursive(sb, indentLevel + 1);
        }
    }

}
