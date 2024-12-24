
using MindustryToolbox.CLI.Commands;
using System.CommandLine;

var root = new RootCommand("MindustryToolbox");

// Get all commands using reflection
var commandTypes = typeof(Program).Assembly.GetTypes()
    .Where(t => t.IsSubclassOf(typeof(BaseCommand)));

// Run setup
foreach (var commandType in commandTypes)
{
    var command = (BaseCommand)Activator.CreateInstance(commandType);
    command.Setup(root);
}
return root.Invoke(args);