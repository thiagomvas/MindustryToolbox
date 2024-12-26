using MindustryToolbox.Core;
using MindustryToolbox.Core.Entities;
using MindustryToolbox.Core.ValueTypes;
using SharpTables;
using System.CommandLine;

namespace MindustryToolbox.CLI.Commands;
public class SectorCommand : BaseCommand
{
    public SectorCommand() : base("sectors", "Get a list of all sectors or only ones that match a certain filter")
    {
    }

    public override void Setup(RootCommand root)
    {
        var resourceFiltersOption = new Option<string>(["--resources", "-r"], "Comma-Separated list of resources to filter the sectors.");
        var difficultyFilterOption = new Option<string>(["--difficulty", "-d"], "Difficulty to filter the sectors. (Low, Medium, High, Eradication)");
        var typeFilterOption = new Option<string>(["--type", "-t"], "Type to filter the sectors.(Attack, Survival)");
        var displayDetailedOption = new Option<bool>(["--detailed", "-a"], "Display detailed information about the sectors.");

        var sectorArgument = new Argument<string>("sector", "Get information about a specific sector rather than a search.")
        {
            Arity = ArgumentArity.ZeroOrOne
        };

        AddOption(resourceFiltersOption);
        AddOption(difficultyFilterOption);
        AddOption(typeFilterOption);
        AddOption(displayDetailedOption);

        AddArgument(sectorArgument);

        this.SetHandler(Execute, sectorArgument, resourceFiltersOption, difficultyFilterOption, typeFilterOption, displayDetailedOption);
        root.AddCommand(this);
    }

    public void Execute(string? selectedSector, string? resources, string? difficulty, string? type, bool detailed)
    {
        var context = new MindustryDbContext();

        Resource resourceFilter = Resource.None;
        var parts = resources?.Split(',').Select(r => r.Trim()).ToArray();

        if (parts != null)
        {
            foreach (var part in parts)
            {
                if (Enum.TryParse<Resource>(part, true, out var resource))
                {
                    resourceFilter |= resource;
                }
            }
        }

        Sector[] sectors;
        if (string.IsNullOrWhiteSpace(selectedSector))
            sectors = context.Sectors.Where(s =>
            (resourceFilter == Resource.None || s.Resources.HasFlag(resourceFilter)) &&
            (string.IsNullOrWhiteSpace(difficulty) || s.Threat.ToString().Equals(difficulty, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrWhiteSpace(type) || s.Type.ToString().Equals(type, StringComparison.OrdinalIgnoreCase)))
            .ToArray();
        else
            sectors = context.Sectors.Where(s => s.Name.Equals(selectedSector, StringComparison.OrdinalIgnoreCase)).ToArray();

        Logger.LogInformation($"Found {sectors.Length} sectors matching the following filters: ");
        if (resourceFilter != Resource.None)
            Logger.LogInformation($"Resources: {resourceFilter}");
        if (!string.IsNullOrWhiteSpace(difficulty))
            Logger.LogInformation($"Difficulty: {difficulty}");
        if (!string.IsNullOrWhiteSpace(type))
            Logger.LogInformation($"Type: {type}");

        var table = new Table();
        if (detailed)
        {

            foreach (var sector in sectors)
            {
                var row = new Row($"{sector.Name} ({sector.Type})",
                    sector.Threat,
                    sector.Resources.HasFlag(Resource.Copper) ? "Y" : "N",
                    sector.Resources.HasFlag(Resource.Lead) ? "Y" : "N",
                    sector.Resources.HasFlag(Resource.Sand) ? "Y" : "N",
                    sector.Resources.HasFlag(Resource.Coal) ? "Y" : "N",
                    sector.Resources.HasFlag(Resource.Titanium) ? "Y" : "N",
                    sector.Resources.HasFlag(Resource.Thorium) ? "Y" : "N",
                    sector.Resources.HasFlag(Resource.Scrap) ? "Y" : "N",
                    sector.Resources.HasFlag(Resource.Water) ? "Y" : "N",
                    sector.Resources.HasFlag(Resource.Oil) ? "Y" : "N",
                    sector.Resources.HasFlag(Resource.Magma) ? "Y" : "N",
                    sector.NumOfWaves,
                    string.Join(',', sector.VulnerableTo));
                table.AddRow(row);
            }
            table.SetHeader("Name", "Difficulty", "Copper", "Lead", "Sand", "Coal", "Titanium", "Thorium", "Scrap", "Water", "Oil", "Magma", "Waves", "Vulnerable to")
                .UsePreset(c =>
                {
                    if (c.Position.X is >= 2 and <= 11)
                    {
                        c.Alignment = Alignment.Center;
                        c.Color = c.Text == "Y" ? ConsoleColor.Green : ConsoleColor.Red;
                    }

                    if (c.Position.X == 1)
                    {
                        if (c.Text == Threat.Low.ToString())
                            c.Color = ConsoleColor.Green;
                        else if (c.Text == Threat.Medium.ToString())
                            c.Color = ConsoleColor.Yellow;
                        else if (c.Text == Threat.High.ToString())
                            c.Color = ConsoleColor.Red;
                        else if (c.Text == Threat.Extreme.ToString())
                            c.Color = ConsoleColor.DarkRed;
                        else if (c.Text == Threat.Eradication.ToString())
                            c.Color = ConsoleColor.DarkMagenta;
                    }

                    if (c.Text == "-1")
                        c.Text = "N/A";
                    if (c.Position.X is 13)
                    {
                        c.Alignment = Alignment.Center;
                    }
                    else if (c.Position.X is 12)
                    {
                        c.Color = ConsoleColor.Yellow;
                        c.Alignment = Alignment.Center;
                    }

                    if (c.Text == "N/A")
                    {
                        c.Alignment = Alignment.Center;
                        c.Color = ConsoleColor.Blue;
                    }
                })
                .UseNullOrEmptyReplacement("None")
                .Write();
        }
        else
        {
            foreach (var sector in sectors)
            {
                var row = new Row(sector.Name, sector.Threat, sector.Resources, sector.Type);
                table.AddRow(row);
            }
            table.SetHeader("Name", "Difficulty", "Resources", "Type")
                .UsePreset(c =>
                {
                    if (c.Position.X == 1)
                    {
                        if (c.Text == Threat.Low.ToString())
                            c.Color = ConsoleColor.Green;
                        else if (c.Text == Threat.Medium.ToString())
                            c.Color = ConsoleColor.Yellow;
                        else if (c.Text == Threat.High.ToString())
                            c.Color = ConsoleColor.Red;
                        else if (c.Text == Threat.Extreme.ToString())
                            c.Color = ConsoleColor.DarkRed;
                        else if (c.Text == Threat.Eradication.ToString())
                            c.Color = ConsoleColor.DarkMagenta;
                    }
                })
                .Write();
        }
    }
}
