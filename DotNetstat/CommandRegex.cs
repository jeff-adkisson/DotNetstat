using System.Text.RegularExpressions;
using DotNetstat.Models;

namespace DotNetstat;

public sealed class CommandRegex
{
    public CommandRegex(ParsingModel model)
    {
        GetProcessesCommand = model.GetProcessesCommand;

        var netstatParserRegex =
            string.IsNullOrWhiteSpace(model.NetstatParserRegex) ? ".*" : model.NetstatParserRegex;
        NetstatParser = new Regex(netstatParserRegex, RegexOptions.Compiled);

        var regexProcessId = string.IsNullOrWhiteSpace(model.ProcessIdParserRegex)
            ? ".*"
            : model.ProcessIdParserRegex;
        ProcessIdParser = new Regex(regexProcessId, RegexOptions.Compiled);

        var regexProcesses = string.IsNullOrWhiteSpace(model.GetProcessesParserRegex)
            ? ".*"
            : model.GetProcessesParserRegex;
        GetProcessesParser = new Regex(regexProcesses, RegexOptions.Compiled | RegexOptions.Multiline);
    }

    public Regex NetstatParser { get; }

    public Regex ProcessIdParser { get; }

    public bool IsProcessIdParsingEnabled => ProcessIdParser.ToString() != ".*";

    public string GetProcessesCommand { get; }

    public Regex GetProcessesParser { get; }
}