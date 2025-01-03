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
            var sectorsFilePath = Path.Combine(AppContext.BaseDirectory, "Resources/Sectors.json");
            Instance.Sectors = Utils.ParseSectors(sectorsFilePath);
        }
        return Instance.Sectors;
    }

    public static IEnumerable<Sector> GetSectors(string sectorJson)
    {
        if (Instance.Sectors is null)
        {
            Instance.Sectors = Utils.ParseSectorsFromJson(sectorJson);
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
            double liquidMultiplier = 1.0;
            if(producer.LiquidBuffs.Length > 0)
            {
                if (flags.HasFlag(BuffFlags.Water) && producer.LiquidBuffs.Any(b => b.Liquid == Resource.Water))
                {
                    var buff = producer.LiquidBuffs.FirstOrDefault(b => b.Liquid == Resource.Water);
                    liquidMultiplier = buff.Multiplier;
                }
                else if (flags.HasFlag(BuffFlags.Cryofluid) && producer.LiquidBuffs.Any(b => b.Liquid == Resource.Cryofluid))
                {
                    var buff = producer.LiquidBuffs.FirstOrDefault(b => b.Liquid == Resource.Cryofluid);
                    liquidMultiplier = buff.Multiplier;
                }
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
            var mult = liquidMultiplier * overdriveMultiplier;
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

}
