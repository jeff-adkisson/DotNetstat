namespace DotNetstat;

public sealed class CommandAttribute : Attribute
{
    public CommandAttribute(string command, string arguments)
    {
        Command = command;
        Arguments = arguments;
    }

    public string Command { get; init; }
    public string Arguments { get; init; }
}