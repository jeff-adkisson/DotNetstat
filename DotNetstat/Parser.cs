using System.Diagnostics;

namespace DotNetstat;

internal sealed class Parser
{
    internal Parser(ICommand command, bool includeProcessDetails)
    {
        Command = command;
        IncludeProcessDetails = includeProcessDetails;
    }

    private ICommand Command { get; }

    private bool IncludeProcessDetails { get; }

    internal INetstatOutput Parse(string netstatCmdOutput)
    {
        var parsedLines = new List<NetstatLine>();
        var unparsedLines = new List<Line>();
        var processes = IncludeProcessDetails ? Processes.Running() : null;

        var lines = netstatCmdOutput.Split('\n');

        for (var index = 0; index < lines.Length; index++)
        {
            var line = lines[index];
            var record = ParseLine(index + 1, line, Command, processes);
            if (record != null)
                parsedLines.Add(record);
            else
                unparsedLines.Add(new Line(index + 1, line));
        }

        return new NetstatOutput(Command, parsedLines, unparsedLines);
    }

    private static NetstatLine? ParseLine(
        int lineNumber,
        string line,
        ICommand command,
        IReadOnlyDictionary<int, Process>? dictionary)
    {
        var match = command.RegexCompiled.Match(line);
        if (!match.Success) return null;
        if (!int.TryParse(match.Groups["pid"].Value, out var processId)) processId = 0;

        if (processId == 0 && command.RegexProcessId != "*")
        {
            var processIdMatch = command.RegexProcessIdCompiled.Match(match.Groups["pid"].Value);
            if (!int.TryParse(processIdMatch.Groups["pid"].Value, out processId)) processId = 0;
        }

        var process = dictionary != null && dictionary.TryGetValue(processId, out var value)
            ? value
            : null;

        return new NetstatLine(lineNumber, line, process)
        {
            Protocol = match.Groups["proto"].Value,
            LocalAddress = match.Groups["local"].Value,
            ForeignAddress = match.Groups["foreign"].Value,
            State = match.Groups["state"].Value,
            ProcessId = processId
        };
    }
}