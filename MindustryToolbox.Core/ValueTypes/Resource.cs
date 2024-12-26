namespace MindustryToolbox.Core.ValueTypes;
[Flags]
public enum Resource
{
    None = 0,
    Copper = 1 << 0,
    Lead = 1 << 1,
    Scrap = 1 << 2,
    Sand = 1 << 3,
    Coal = 1 << 4,
    Titanium = 1 << 5,
    Thorium = 1 << 6,
    SporePod = 1 << 7,
    Graphite = 1 << 8,
    Silicon = 1 << 9,
    Metaglass = 1 << 10,
    Plastanium = 1 << 11,
    PhaseFabric = 1 << 12,
    SurgeAlloy = 1 << 13,
    Pyratite = 1 << 14,
    BlastCompound = 1 << 15,
    Water = 1 << 16,
    Oil = 1 << 17,
    Cryofluid = 1 << 18,
    Slag = 1 << 19,
    Magma = 1 << 20,
}
