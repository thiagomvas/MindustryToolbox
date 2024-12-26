using System.Text;

namespace MindustryToolbox.Core.Entities;
public class ProductionRecipe
{
    /// <summary>
    /// Gets the parent production node.
    /// </summary>
    public ProductionNode Parent { get; init; }

    /// <summary>
    /// Gets the structure associated with this recipe.
    /// </summary>
    public Structure Structure { get; init; }

    /// <summary>
    /// Gets the output rate per second for this recipe.
    /// </summary>
    public double OutputPerSecond { get; init; }

    /// <summary>
    /// Gets or sets the list of input production nodes.
    /// </summary>
    public List<ProductionNode> Inputs { get; set; } = new();

    /// <summary>
    /// Gets the amount needed to achieve the specified output rate.
    /// </summary>
    public readonly int AmountNeeded;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductionRecipe"/> class with the specified parent node, structure, and output rate.
    /// </summary>
    /// <param name="parent">The parent production node.</param>
    /// <param name="structure">The structure associated with this recipe.</param>
    /// <param name="outputPerSecond">The output rate per second.</param>
    public ProductionRecipe(ProductionNode parent, Structure structure, double outputPerSecond)
    {
        Parent = parent;
        Structure = structure;
        OutputPerSecond = outputPerSecond;

        var output = structure.Outputs.First(o => o.Resource == parent.Resource);
        AmountNeeded = (int)Math.Ceiling(outputPerSecond / output.Rate);
    }

    /// <summary>
    /// Returns a string representation of the production recipe.
    /// </summary>
    /// <returns>A string representation of the production recipe.</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        ToStringRecursive(sb, 0);
        return sb.ToString();
    }

    /// <summary>
    /// Recursively builds a string representation of the production recipe and its nested inputs.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> to append the string representation to.</param>
    /// <param name="indentLevel">The current level of indentation.</param>
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
