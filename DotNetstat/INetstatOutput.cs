using DotNetstat.NetstatParsing;

namespace DotNetstat;

public interface INetstatOutput
{
    string OriginalOutput { get; }

    IReadOnlyCollection<NetstatLine> Lines { get; init; }

    IReadOnlyCollection<Line> UnparsedLines { get; init; }

    ICommand Command { get; init; }

    bool Success { get; init; }

    string Error { get; init; }
}