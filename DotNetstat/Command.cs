using System.Text.RegularExpressions;

namespace DotNetstat;

public class Command : ICommand
{
    private Regex? _regex;
    private Regex? _regexProcessId;

    public string RegexProcessId { get; init; } = "*";

    public Regex RegexProcessIdCompiled => _regexProcessId ??= new Regex(RegexProcessId, RegexOptions.Compiled);

    public int PlatformId { get; init; }

    public string Platform => PlatformEnum.ToString().ToLower();

    public Platform PlatformEnum => (Platform)PlatformId;

    public string Id { get; init; } = "";

    public string Name { get; init; } = "";

    public string Arguments { get; init; } = "";

    public int Priority { get; init; }

    public bool IsPlatformDefault => Priority == 1;

    public string Regex { get; init; } = "*";

    public Regex RegexCompiled => _regex ??= new Regex(Regex, RegexOptions.Compiled);
}