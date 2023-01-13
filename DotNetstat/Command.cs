using DotNetstat.Models;

namespace DotNetstat;

public sealed class Command : ICommand
{
    public Command(CommandModel model)
    {
        Platform = model.Platform;
        Shell = model.Shell;
        Name = model.Name;
        Arguments = model.Arguments;
        Regex = new CommandRegex(model.Parsing);
    }

    public Platform Platform { get; init; }

    public string Shell { get; init; }
    public string Name { get; init; }
    public string Arguments { get; init; }
    public CommandRegex Regex { get; init; }
}