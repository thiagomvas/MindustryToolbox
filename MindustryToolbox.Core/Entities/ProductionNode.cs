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

    public IEnumerable<ResourceRate> GetCombinedRequiredInputRates()
    {
        var ratesWithDepth = new Dictionary<Resource, (double Rate, int Depth)>();

        // Recursive helper method to calculate the input rates, including nested resources
        void CalculateInputRates(ProductionRecipe recipe, double multiplier, int depth)
        {
            foreach (var input in recipe.Inputs)
            {
                double requiredRate = input.OutputPerSecond * multiplier;

                if (ratesWithDepth.ContainsKey(input.Resource))
                {
                    var (existingRate, existingDepth) = ratesWithDepth[input.Resource];
                    ratesWithDepth[input.Resource] = (existingRate + requiredRate, Math.Max(existingDepth, depth));
                }
                else
                {
                    ratesWithDepth[input.Resource] = (requiredRate, depth);
                }

                foreach (var nestedRecipe in input.Recipes)
                {
                    // Recurse into nested recipes with the correct multiplier and depth
                    CalculateInputRates(nestedRecipe, requiredRate / input.OutputPerSecond, depth + 1);
                }
            }
        }

        foreach (var recipe in Recipes)
        {
            // Start the calculation with the recipe's output per second as the multiplier and depth 0
            CalculateInputRates(recipe, 1, 0);
        }

        var sortedResults = ratesWithDepth
            .Select(pair => new ResourceRate
            {
                Resource = pair.Key,
                Rate = pair.Value.Rate
            })
            .OrderBy(r => ratesWithDepth[r.Resource].Depth)
            .ToList();

        return sortedResults;
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
