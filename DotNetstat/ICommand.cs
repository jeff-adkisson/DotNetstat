using System.Text.RegularExpressions;

namespace DotNetstat;

public interface ICommand
{
    int PlatformId { get; init; }
    string Platform { get; }
    Platform PlatformEnum { get; }
    string Id { get; init; }
    string Name { get; init; }
    string Arguments { get; init; }
    int Priority { get; init; }
    bool IsPlatformDefault { get; }
    public string Regex { get; init; }
    public Regex RegexCompiled { get; }
    public string RegexProcessId { get; init; }
    public Regex RegexProcessIdCompiled { get; }
}