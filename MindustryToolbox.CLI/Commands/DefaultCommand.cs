using System.CommandLine;

namespace MindustryToolbox.CLI.Commands;
public class DefaultCommand : BaseCommand
{
    public DefaultCommand() : base("default", "Ran by default on root")
    {
    }

    public override void Setup(RootCommand root)
    {
        var heyOption = new Option<bool>("--hey", "Says hey instead of hello");
        var nameOption = new Option<string>("--name", "Name to greet");

        AddOption(heyOption);
        AddOption(nameOption);

        this.SetHandler(Execute, heyOption, nameOption);

        root.Add(this);
    }

    public void Execute(bool useHey, string name)
    {
        Console.WriteLine($"{(useHey ? "Hey" : "Hello")} {(string.IsNullOrWhiteSpace(name) ? "DefaultCommand!" : name)}");
    }
}
