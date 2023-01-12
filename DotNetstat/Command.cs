using DotNetstat.Models;

namespace DotNetstat;

public sealed class Command : ICommand
{
    public Command(Platform platform, string name, string arguments = "", int priority = 1, string? id = null,
        Parsing? parsing = null)
    {
        arguments ??= "";
        id ??= Guid.NewGuid().ToString();
        parsing ??= new Parsing(new ParsingModel());

        Platform = platform;
        Id = id;
        Name = name;
        Arguments = arguments;
        Priority = priority;
        Parsing = parsing;
    }

    public Command(CommandModel model)
    {
        Platform = model.Platform;
        Id = model.Id;
        Name = model.Name;
        Arguments = model.Arguments;
        Priority = model.Priority;
        Parsing = new Parsing(model.Parsing);
    }

    public Platform Platform { get; init; }
    public string Id { get; init; }
    public string Name { get; init; }
    public string Arguments { get; init; }
    public int Priority { get; init; }
    public bool IsPlatformDefault => Priority == 1;
    public Parsing Parsing { get; init; }
}