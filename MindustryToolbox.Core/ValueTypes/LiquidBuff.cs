namespace MindustryToolbox.Core.ValueTypes;
public readonly record struct LiquidBuff
{
    /// <summary>
    /// Gets the liquid resource associated with the buff.
    /// </summary>
    public Resource Liquid { get; init; }
    /// <summary>
    /// Gets the multiplier effect of the buff.
    /// </summary>
    public double Multiplier { get; init; }
    /// <summary>
    /// Gets the rate at which the liquid must enter.
    /// </summary>
    public double Rate { get; init; }
}
