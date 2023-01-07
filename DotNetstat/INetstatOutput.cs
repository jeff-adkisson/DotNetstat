namespace DotNetstat;

public interface INetstatOutput
{
    string OriginalOutput { get; init; }
    
    IReadOnlyCollection<NetstatLine> Lines { get; init; }

    ICommand Command { get; init; }

    bool Success { get; init; }

    string Error { get; init; }
}