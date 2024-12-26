namespace MindustryToolbox.Core.ValueTypes;
public readonly record struct ResourceRate
{
    /// <summary>
    /// Gets the resource.
    /// </summary>
    public Resource Resource { get; init; }
    /// <summary>
    /// Gets the rate of the resource.
    /// </summary>
    public double Rate { get; init; }
}
