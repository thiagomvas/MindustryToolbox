using MindustryToolbox.Core.Entities;
using MindustryToolbox.Core.ValueTypes;
using System.Text;

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
        if (Instance.Sectors is null)
        {
            Instance.Sectors = Utils.ParseSectorsFromText(sectorText.Split('\n'));
        }
        return Instance.Sectors;
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

    public static IEnumerable<Structure> GetStructures(string structuresJson)
    {
        if (Instance.Structures is null)
        {
            Instance.Structures = Utils.ParseStructuresFromText(structuresJson);
        }
        return Instance.Structures;
    }

    public static ProductionNode CalculateProduction(Resource resource, double resourcesPerSecExpected, BuffFlags flags = BuffFlags.NoLiquidOrOverdrive)
    {
        var root = new ProductionNode(resource, resourcesPerSecExpected);
        return CalculateProductionRecursive(resource, resourcesPerSecExpected, root, flags);

    }

    private static ProductionNode CalculateProductionRecursive(Resource resource, double resourcesPerSecExpected, ProductionNode node, BuffFlags flags = BuffFlags.NoLiquidOrOverdrive)
    {
        var producers = GetStructures().Where(s => s.Outputs.Any(o => o.Resource == resource));

        foreach (var producer in producers)
        {
            var output = producer.Outputs.First(o => o.Resource == resource);

            // Apply Liquid Buff (if any) by checking the LiquidBuffs on the structure
            double outputMultiplier = 1.0;
            if (flags.HasFlag(BuffFlags.Water))
            {
                var buff = producer.LiquidBuffs.FirstOrDefault(b => b.Liquid == Resource.Water);
                outputMultiplier = buff.Multiplier;
            }
            else if (flags.HasFlag(BuffFlags.Cryofluid))
            {
                var buff = producer.LiquidBuffs.FirstOrDefault(b => b.Liquid == Resource.Cryofluid);
                outputMultiplier = buff.Multiplier;
            }

            // Apply Overdrive Buff (if any) for Dome or Projector
            double overdriveMultiplier = 1.0;

            if ((flags & BuffFlags.NoOverdrive) == 0)
            {
                if ((flags & BuffFlags.OverdriveProjector) > 0)
                {
                    overdriveMultiplier = 1.5;

                }
                else if ((flags & BuffFlags.OverdriveDome) > 0)
                {
                    overdriveMultiplier = 2.5;

                }
            }

            // Adjust the output rate based on buffs
            var mult = outputMultiplier * overdriveMultiplier;
            double adjustedOutputRate = output.Rate * mult;
            var numProducersNeeded = Math.Ceiling(resourcesPerSecExpected / adjustedOutputRate);
            var recipe = new ProductionRecipe(node, producer, numProducersNeeded * adjustedOutputRate, mult);
            node.Recipes.Add(recipe);

            foreach (var requiredInput in producer.Inputs)
            {
                var childNode = new ProductionNode(requiredInput.Resource, requiredInput.Rate * numProducersNeeded * mult);
                recipe.Inputs.Add(childNode);
                CalculateProductionRecursive(requiredInput.Resource, requiredInput.Rate * numProducersNeeded * mult, childNode, flags);  // Pass the flags for nested buffs
            }
        }

        return node;
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
