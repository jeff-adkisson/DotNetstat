using System.Collections.ObjectModel;
using DotNetstat.Configuration;

namespace DotNetstat;

public static class Commands
{
    private static ReadOnlyCollection<ICommand>? ReadOnlyCommands { get; set; }

    public static ReadOnlyCollection<ICommand> Items =>
        ReadOnlyCommands ??= Loader.GetConfiguration();

    public static ICommand GetCommand(this Platform platform)
    {
        if (platform == Platform.Automatic)
            platform = PlatformDetector.Detect();

        var command = Items.FirstOrDefault(c => c.Platform == platform);
        if (command == null) throw new ArgumentException($"No command found for platform [{platform}]");
        return command;
    }
}