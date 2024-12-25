using MindustryToolbox.Core;
using MindustryToolbox.Core.Entities;
using MindustryToolbox.Core.ValueTypes;

var structures = Mindustry.GetStructures();

Console.WriteLine(Mindustry.GetNumOfDrillNeededForResourcePerSecond(Resource.Copper, 10));