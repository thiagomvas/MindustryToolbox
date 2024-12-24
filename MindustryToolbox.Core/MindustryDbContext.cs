using Microsoft.EntityFrameworkCore;
using MindustryToolbox.Core.Entities;
using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public void ParseSectors(string sectorText)
    {
        Sectors = Utils.ParseSectorsFromText(sectorText.Split('\n'));
    }

}