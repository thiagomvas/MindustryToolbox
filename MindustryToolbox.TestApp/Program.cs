using MindustryToolbox.Core;
using MindustryToolbox.Core.ValueTypes;

var structures = Mindustry.GetStructures();

var node = Mindustry.CalculateProduction(Resource.Copper, 10);

Console.WriteLine(node);

var rates = node.GetCombinedRequiredInputRates();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

foreach (var rate in rates)
{
    Console.WriteLine($"{rate.Rate} {rate.Resource}/sec");
}
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine("WITH OVERDRIVE");
var node2 = Mindustry.CalculateProduction(Resource.Copper, 10, BuffFlags.Water);

Console.WriteLine(node2);

var rates2 = node2.GetCombinedRequiredInputRates();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

foreach (var rate in rates2)
{
    Console.WriteLine($"{rate.Rate} {rate.Resource}/sec");
}

