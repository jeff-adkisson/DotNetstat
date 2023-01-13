using System.Text.RegularExpressions;
using DotNetstat.Models;

namespace DotNetstat;

public sealed class Command : ICommand
{
    public Command(CommandModel model)
    {
        Platform = model.Platform;
        Shell = model.Shell;
        Name = model.Name;
        Arguments = model.Arguments;
        Parsing = new ParsingRegex(model.Parsing);
    }

    public Platform Platform { get; init; }
    
    public string Shell { get; init; }
    public string Name { get; init; }
    public string Arguments { get; init; }
    public ParsingRegex Parsing { get; init; }

    public sealed class ParsingRegex
    {
        public ParsingRegex(ParsingModel model)
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
            GetProcessesParser = new Regex(regexProcesses, RegexOptions.Compiled);
        }

        public Regex NetstatParser { get; }

        public Regex ProcessIdParser { get; }

        public bool IsProcessIdParsingEnabled => ProcessIdParser.ToString() != ".*";

        public string GetProcessesCommand { get; }

        public Regex GetProcessesParser { get; }
    }
}