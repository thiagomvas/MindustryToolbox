using MindustryToolbox.Core;
using MindustryToolbox.Core.ValueTypes;

var structures = Mindustry.GetStructures();

var impactReactor = structures.FirstOrDefault(s => s.Name == "Impact Reactor");

var inputRequired = Mindustry.GetInputRequiredToFuelStructures(impactReactor, 4, BuffFlags.OverdriveProjector);

foreach(var node in inputRequired)
{
    Console.WriteLine($"{node.Resource} : {node.OutputPerSecond}");
}
