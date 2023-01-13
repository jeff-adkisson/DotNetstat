using DotNetstat.NetstatParsing;

namespace DotNetstat;

public interface IOutput
{
    string OriginalOutput { get; }

    IReadOnlyCollection<Line> Lines { get; init; }

    IReadOnlyCollection<OriginalLine> UnparsedLines { get; init; }

    ICommand Command { get; init; }

    bool Success { get; init; }

    string Error { get; init; }
}