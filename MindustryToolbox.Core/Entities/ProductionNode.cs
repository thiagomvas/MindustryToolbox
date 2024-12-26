using MindustryToolbox.Core.ValueTypes;
using System.Text;

namespace MindustryToolbox.Core.Entities;
/// <summary>
/// Represents a node in the production chain, responsible for producing a specific resource.
/// </summary>
public class ProductionNode
{
    /// <summary>
    /// Gets or sets the resource produced by this node.
    /// </summary>
    public Resource Resource { get; set; }

    /// <summary>
    /// Gets or sets the output rate of the resource per second.
    /// </summary>
    public double OutputPerSecond { get; set; }

    /// <summary>
    /// Gets or sets the list of production recipes associated with this node.
    /// </summary>
    public List<ProductionRecipe> Recipes { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductionNode"/> class with the specified resource and output rate.
    /// </summary>
    /// <param name="resource">The resource produced by this node.</param>
    /// <param name="outputPerSecond">The output rate of the resource per second.</param>
    public ProductionNode(Resource resource, double outputPerSecond)
    {
        Resource = resource;
        OutputPerSecond = outputPerSecond;
    }

    /// <summary>
    /// Returns a string representation of the production node and its nested recipes.
    /// </summary>
    /// <returns>A string representation of the production node.</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        ToStringRecursive(sb, 0);
        return sb.ToString();
    }

    /// <summary>
    /// Calculates the combined required input rates for all recipes, including nested resources.
    /// </summary>
    /// <returns>An enumerable of <see cref="ResourceRate"/> representing the combined required input rates.</returns>
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

    /// <summary>
    /// Recursively builds a string representation of the production node and its nested recipes.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> to append the string representation to.</param>
    /// <param name="indentLevel">The current level of indentation.</param>
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
