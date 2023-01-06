namespace DotNetstat;

public class Command
{
    public Command(string commandToParse)
    {
        var components = commandToParse.Split(' ');
        if (components.Length < 2) 
            throw new ArgumentException($"Invalid command: {commandToParse}");

        Name = components[0];
        Arguments = string.Join(' ', components[1..]);
    }

    public Command(string name, string arguments)
    {
        Name = name;
        Arguments = arguments;
    }

    public string Name { get; init; }

    public string Arguments { get; init; }
}