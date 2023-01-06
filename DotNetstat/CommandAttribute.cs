namespace DotNetstat;

public sealed class CommandAttribute : Attribute
{
    public string Command { get; }
    public string Arguments { get; }

    public CommandAttribute(string command, string arguments)
    {
        Command = command;
        Arguments = arguments;
    }
}