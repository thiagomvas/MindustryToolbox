namespace MindustryToolbox.Core.ValueTypes;
public readonly record struct LiquidBuff
{
    /// <summary>
    /// Gets the liquid resource associated with the buff.
    /// </summary>
    public readonly Resource Liquid;
    /// <summary>
    /// Gets the multiplier effect of the buff.
    /// </summary>
    public readonly double Multiplier;
    /// <summary>
    /// Gets the rate at which the liquid must enter.
    /// </summary>
    public readonly double Rate;

    public LiquidBuff()
    {
        this.Liquid = Resource.None;
        Multiplier = 1;
        Rate = 0;
    }
}
