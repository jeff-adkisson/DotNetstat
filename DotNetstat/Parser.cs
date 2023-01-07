using System.Diagnostics;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace DotNetstat;

internal class Parser
{
    public Parser(ICommand command, bool includeProcessDetails)
    {
        Command = command;
        IncludeProcessDetails = includeProcessDetails;
    }

    private ICommand Command { get; }

    private bool IncludeProcessDetails { get; init; }

    public INetstatOutput Parse(string netstatCmdOutput)
    {
        var records = new List<NetstatLine>();
        var processes = IncludeProcessDetails ? Processes.Running() : null;

        var lines = netstatCmdOutput.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        for (var index = 0; index < lines.Length; index++)
        {
            var line = lines[index];
            var record = ParseLine(line, Command, processes);
            if (record != null) records.Add(record);
        }

        return new NetstatOutput(Command, netstatCmdOutput, records.AsReadOnly());
    }

    private static NetstatLine? ParseLine(
        string line,
        ICommand command,
        Dictionary<int, Process>? dictionary)
    {
        var match = command.RegexCompiled.Match(line);
        if (!match.Success) return null;
        if (!int.TryParse(match.Groups["pid"].Value, out var processId)) processId = 0;
        var process = dictionary != null && dictionary.TryGetValue(processId, out var value)
            ? value
            : null;

        return new NetstatLine(process)
        {
            Protocol = match.Groups["proto"].Value,
            LocalAddress = match.Groups["local"].Value,
            ForeignAddress = match.Groups["foreign"].Value,
            State = match.Groups["state"].Value,
            ProcessId = processId
        };
    }
}