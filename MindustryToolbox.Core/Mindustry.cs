using MindustryToolbox.Core.Entities;
using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryToolbox.Core;
public class Mindustry
{
    private IEnumerable<Sector> Sectors;
    private IEnumerable<Structure> Structures;

    #region Singleton
    // Singleton
    private static Mindustry instance;
    internal static Mindustry Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Mindustry();
            }
            return instance;
        }
    }
    private Mindustry()
    {

    }

    #endregion


    public static IEnumerable<Sector> GetSectors()
    {
        if (Instance.Sectors is null)
        {
            var sectorsFilePath = Path.Combine(AppContext.BaseDirectory, "Resources/Sectors.txt");
            Instance.Sectors = Utils.ParseSectors(sectorsFilePath);
        }
        return Instance.Sectors;
    }

    public static IEnumerable<Sector> GetSectors(string sectorText)
    {
        return Utils.ParseSectorsFromText(sectorText.Split('\n'));
    }

    public static Structure GetStructure(string json)
    {
        return Structure.FromJson(json);
    }

    public static IEnumerable<Structure> GetStructures()
    {
        if (Instance.Structures is null)
        {
            var structuresFilePath = Path.Combine(AppContext.BaseDirectory, "Resources/Structures.json");
            Instance.Structures = Utils.ParseStructures(structuresFilePath);
        }
        return Instance.Structures;
    }

    public static string GetNumOfDrillNeededForResourcePerSecond(Resource resource, float resourcesPerSecExpected)
    {
        // Retrieve all structures that produce the target resource
        var producers = GetStructures().Where(s => s.Outputs.Any(o => o.Resource == resource));

        var sb = new StringBuilder();

        foreach (var producer in producers)
        {
            // Find the rate at which the producer outputs the resource
            var output = producer.Outputs.First(o => o.Resource == resource);

            // Calculate how many producers are needed to meet the expected rate of the resource
            var numProducersNeeded = Math.Ceiling(resourcesPerSecExpected / output.Rate);

            sb.AppendLine($"{producer.Name} needed: {numProducersNeeded} to meet {resourcesPerSecExpected} {resource}/s.");

            // Iterate through each input required by the producer
            foreach (var requiredInput in producer.Inputs)
            {
                sb.AppendLine($"  - {requiredInput.Resource} needed: {requiredInput.Rate * numProducersNeeded}/s");

                // Find the producers that can supply the required input resource
                var inputProducers = GetStructures().Where(s => s.Outputs.Any(o => o.Resource == requiredInput.Resource)).ToList();

                // For each input resource, calculate how many producers are needed
                foreach (var inputProducer in inputProducers)
                {
                    var inputProducerOutput = inputProducer.Outputs.First(o => o.Resource == requiredInput.Resource);
                    var numOfInputNeeded = Math.Ceiling((requiredInput.Rate * numProducersNeeded) / inputProducerOutput.Rate);
                    sb.AppendLine($"    {inputProducer.Name} needed: {numOfInputNeeded} to meet {requiredInput.Rate * numProducersNeeded}/s.");
                }
            }
        }

        return sb.ToString();
    }

}
