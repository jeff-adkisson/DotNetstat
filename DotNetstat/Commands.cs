using System.Collections.ObjectModel;
using System.Text.Json;
using DotNetstat.Resources;

namespace DotNetstat;

public static class Commands
{
    private const string CommandsFile = "commands.json";

    private static ReadOnlyCollection<ICommand>? _readOnlyCommands;

    public static ReadOnlyCollection<ICommand> All
    {
        get
        {
            if (_readOnlyCommands != null) return _readOnlyCommands;

            var commandJson = ResourceGetter.Get(CommandsFile);
            var commands = JsonSerializer.Deserialize<List<Command>>(commandJson);
            return _readOnlyCommands = commands!.Cast<ICommand>().ToList().AsReadOnly();
        }
    }

    public static ICommand DefaultByPlatform(Platform platform)
    {
        if (platform == Platform.Automatic)
            platform = PlatformAutoSelector.Select();

        var command = All.FirstOrDefault(c => c.PlatformId == (int)platform && c.IsPlatformDefault);
        if (command == null) throw new Exception($"No default command found for platform [{platform}]");
        return command;
    }

    public static ICommand? ById(string id)
    {
        id = (id ?? "").Trim();
        return All.FirstOrDefault(c => c.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
    }
}