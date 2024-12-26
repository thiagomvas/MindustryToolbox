using MindustryToolbox.Core;
using MindustryToolbox.Core.ValueTypes;

var structures = Mindustry.GetStructures();

var node = Mindustry.CalculateProduction(Resource.BlastCompound, 10);

Console.WriteLine(node);

var rates = node.GetCombinedRequiredInputRates();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

foreach (var rate in rates)
{
    Console.WriteLine($"{rate.Rate} {rate.Resource}/sec");
}

