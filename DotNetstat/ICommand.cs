namespace DotNetstat;

public interface ICommand
{
    Platform Platform { get; }
    string Id { get; }
    string Name { get; }
    string Arguments { get; }
    int Priority { get; }
    bool IsPlatformDefault { get; }
    Parsing Parsing { get; }
}