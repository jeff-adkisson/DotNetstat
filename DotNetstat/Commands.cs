using System.Collections.ObjectModel;
using System.Text.Json;
using DotNetstat.Resources;

namespace DotNetstat;

public static class Commands
{
    private const string CommandsFile = "commands.json";

    private static ReadOnlyCollection<ICommand>? ReadOnlyCommands { get; set; }

    public static ReadOnlyCollection<ICommand> Items
    {
        get
        {
            if (ReadOnlyCommands != null) return ReadOnlyCommands;

            var commandJson = ResourceGetter.Get(CommandsFile);
            var commands = JsonSerializer.Deserialize<List<Command>>(commandJson);
            return ReadOnlyCommands = commands!.Cast<ICommand>().ToList().AsReadOnly();
        }
    }

    public static ICommand DefaultByPlatform(Platform platform)
    {
        if (platform == Platform.Automatic)
            platform = PlatformAutoSelector.Select();

        var command = Items.FirstOrDefault(c => c.PlatformId == (int)platform && c.IsPlatformDefault);
        if (command == null) throw new ArgumentException($"No default command found for platform [{platform}]");
        return command;
    }

    public static ICommand? ById(string id)
    {
        id = (id ?? "").Trim();
        return Items.FirstOrDefault(c => c.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
    }
}