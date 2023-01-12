using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetstat.Models;
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

            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            var commands = JsonSerializer.Deserialize<List<CommandModel>>(commandJson, options);
            return ReadOnlyCommands = commands!.Select(c => new Command(c) as ICommand).ToList().AsReadOnly();
        }
    }

    public static ICommand DefaultByPlatform(Platform platform)
    {
        if (platform == Platform.Automatic)
            platform = PlatformDetector.Detect();

        var command = Items.FirstOrDefault(c => c.Platform == platform && c.IsPlatformDefault);
        if (command == null) throw new ArgumentException($"No default command found for platform [{platform}]");
        return command;
    }

    public static ICommand? ById(string id)
    {
        id = (id ?? "").Trim();
        return Items.FirstOrDefault(c => c.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
    }
}