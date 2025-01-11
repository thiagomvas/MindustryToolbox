using MindustryToolbox.Core;

namespace MindustryToolbox.Tests;

[TestFixture]
public class DataValidation
{
    [Test]
    public void StructuresHaveUniqueNames()
    {
        var structures = Mindustry.GetStructures();
        var names = structures.Select(s => s.Name);
        
        Assert.That(names, Is.Unique);
    }

    [Test]
    public void SectorsHaveUniqueNames()
    {
        var sectors = Mindustry.GetSectors();
        var names = sectors.Select(s => s.Name);
        
        Assert.That(names, Is.Unique);
    }
}