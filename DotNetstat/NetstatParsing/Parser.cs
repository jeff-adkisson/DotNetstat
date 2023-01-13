using System.Diagnostics;

namespace DotNetstat.NetstatParsing;

internal sealed class Parser
{
    internal Parser(ICommand command, bool includeProcessDetails)
    {
        Command = command;
        IncludeProcessDetails = includeProcessDetails;
    }

    private ICommand Command { get; }

    private bool IncludeProcessDetails { get; }

    internal IOutput Parse(string netstatCmdOutput)
    {
        var parsedLines = new List<Line>();
        var unparsedLines = new List<OriginalLine>();
        var processes = IncludeProcessDetails ? Processes.GetRunning() : null;

        var lines = netstatCmdOutput.Split('\n');

        for (var index = 0; index < lines.Length; index++)
        {
            var line = lines[index];
            var record = ParseLine(index + 1, line, Command, processes);
            if (record != null)
                parsedLines.Add(record);
            else
                unparsedLines.Add(new OriginalLine(index + 1, line));
        }

        return new Output(Command, parsedLines, unparsedLines);
    }

    private static Line? ParseLine(
        int lineNumber,
        string line,
        ICommand command,
        IReadOnlyDictionary<int, Process>? dictionary)
    {
        var cmd = (Command)command;
        var match = cmd.Regex.NetstatParser.Match(line);
        if (!match.Success) return null;
        if (!int.TryParse(match.Groups["pid"].Value, out var processId)) processId = 0;

        if (processId == 0 && command.Regex.IsProcessIdParsingEnabled)
        {
            var processIdMatch = cmd.Regex.ProcessIdParser.Match(match.Groups["pid"].Value);
            if (!int.TryParse(processIdMatch.Groups["pid"].Value, out processId)) processId = 0;
        }

        var process = dictionary != null && dictionary.TryGetValue(processId, out var value)
            ? value
            : null;

        return new Line(lineNumber, line, process)
        {
            Protocol = match.Groups["proto"].Value,
            LocalAddress = new Address(match.Groups["local"].Value),
            ForeignAddress = new Address(match.Groups["foreign"].Value),
            State = match.Groups["state"].Value,
            ProcessId = processId
        };
    }
}