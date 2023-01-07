using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DotNetstat;

public class Command : ICommand
{
    private Regex? _regex;

    public Command(Platform platform, string id, string name, string arguments, int priority, string regex)
        : this((int)platform, id, name, arguments, priority, regex)
    {
    }

    public Command(int platformId, string id, string name, string arguments, int priority, string regex)
    {
        PlatformId = platformId;
        Id = id;
        Name = name;
        Arguments = arguments;
        Priority = priority;
        Regex = regex;
    }
    
#nullable disable
    [JsonConstructor]
    public Command()
    {
        //json constructor
    }

    public int PlatformId { get; init; }

    public string Platform => PlatformEnum.ToString().ToLower();

    public Platform PlatformEnum => (Platform)PlatformId;

    public string Id { get; init; }

    public string Name { get; init; }

    public string Arguments { get; init; }

    public int Priority { get; init; }

    public bool IsPlatformDefault => Priority == 1;
    
    public string Regex { get; init; }

    public Regex RegexCompiled => _regex ??= new Regex(Regex, RegexOptions.Compiled);
}