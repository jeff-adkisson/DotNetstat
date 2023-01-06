using System.Text.RegularExpressions;

namespace DotNetstat;

internal partial class NetstatParserWindows : INetstatParser
{
    public IEnumerable<NetstatLine> Parse(string netstatCmdOutput)
    {
        var records = new List<NetstatLine>();

        var lines = netstatCmdOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        for (var index = 0; index < lines.Length; index++)
        {
            var line = lines[index];
            
            if (!string.IsNullOrWhiteSpace(line) && line.Trim().ToLower().StartsWith("proto")) 
                continue;
            
            var record = ParseLine(line);
            if (record != null) records.Add(record);
        }

        return records;
    }

    private static NetstatLine? ParseLine(string line)
    {
        // Use a regular expression to parse the line
        var match = ExtractNetstatRecordRegex().Match(line);
        if (match.Success)
        {
            return new NetstatLine
            {
                Protocol = match.Groups["proto"].Value,
                LocalAddress = match.Groups["local"].Value,
                ForeignAddress = match.Groups["foreign"].Value,
                State = match.Groups["state"].Value,
                ProcessId = int.Parse(match.Groups["pid"].Value)
            };
        }
        return null;
    }

    [GeneratedRegex("^\\s*(?<proto>\\S+)\\s+(?<local>\\S+)\\s+(?<foreign>\\S+)\\s+(?<state>\\S+)\\s+(?<pid>\\d+)")]
    private static partial Regex ExtractNetstatRecordRegex();
}