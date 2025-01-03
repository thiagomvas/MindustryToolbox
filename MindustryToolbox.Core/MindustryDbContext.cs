namespace MindustryToolbox.Core;
public class MindustryDbContext
{
    public List<Entities.Sector> Sectors { get; set; } = new();

    public MindustryDbContext()
    {
    }

    public void FetchSectors()
    {
        // Get the resource path for the sectors file, it is located wherever the built files are
        var sectorsFilePath = Path.Combine(AppContext.BaseDirectory, "Resources/Sectors.txt");
        Sectors = Utils.ParseSectors(sectorsFilePath);
    }
    public void ParseSectors(string sectorJson)
    {
        Sectors = Utils.ParseSectorsFromJson(sectorJson);
    }

}