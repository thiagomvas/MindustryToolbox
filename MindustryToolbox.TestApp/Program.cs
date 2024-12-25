using MindustryToolbox.Core;
using MindustryToolbox.Core.ValueTypes;

var structures = Mindustry.GetStructures();

Console.WriteLine(Mindustry.CalculateProduction(Resource.Silicon, 10));