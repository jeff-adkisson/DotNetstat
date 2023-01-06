using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DotNetstat;

internal partial class NetstatParserWindows : NetstatParserBase
{
    public NetstatParserWindows(bool includeProcessDetails)
        : base(includeProcessDetails)
    {
    }

    public override IEnumerable<NetstatLine> Parse(string netstatCmdOutput)
    {
        var records = new List<NetstatLine>();
        var processes = IncludeProcessDetails ? Processes.GetRunningProcesses() : null;

        var lines = netstatCmdOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        for (var index = 0; index < lines.Length; index++)
        {
            var line = lines[index];

            if (!string.IsNullOrWhiteSpace(line) && line.Trim().ToLower().StartsWith("proto"))
                continue;

            var record = ParseLine(line, processes);
            if (record != null) records.Add(record);
        }

        return records;
    }

    private static NetstatLine? ParseLine(
        string line,
        Dictionary<int, Process>? dictionary)
    {
        var match = ExtractNetstatRecordRegex().Match(line);
        if (!match.Success) return null;
        var processId = int.Parse(match.Groups["pid"].Value);
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

    [GeneratedRegex("^\\s*(?<proto>\\S+)\\s+(?<local>\\S+)\\s+(?<foreign>\\S+)\\s+(?<state>\\S+)\\s+(?<pid>\\d+)")]
    private static partial Regex ExtractNetstatRecordRegex();
}